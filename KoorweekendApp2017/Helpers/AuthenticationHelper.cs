using System;
using System.Threading.Tasks;
using KoorweekendApp2017.Enums;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public class AuthenticationHelper
	{
		public async static Task<bool> RegisterDevice(String emailaddress)
		{
			CustomDevice device = new CustomDevice(CrossDeviceInfo.Current);

			string url = String.Format(
				"http://www.jongerenkooronevoice.nl/authentication/registerdevice/{0}",
				emailaddress
			);
			bool mailSend = await RestHelper.PostDataToUrl<bool>(url, device);

			if (!mailSend)
			{
				var tryAgain = await Application.Current.MainPage.DisplayAlert("Mail niet verzonden", "Staat je internet aan? De bevestigingsmail kon niet worden verzonden.", "Annuleren", "Probeer opnieuw");
				if (tryAgain) await RegisterDevice(emailaddress);
			}

			return mailSend;
		}

		public async static Task<AuthenticationResult> GetAuthenticationResult(String emailaddress)
		{
			var authResult = await RestHelper.GetRestDataFromUrl<AuthenticationResult>(
				String.Format(
					"http://www.jongerenkooronevoice.nl/authentication/checkcredentials?EmailAddress={0}&DeviceId={1}",
					emailaddress,
					new CustomDevice(CrossDeviceInfo.Current).Id
				)
			);

			return authResult;
		}

		public async static Task<bool> IsAuthenticated()
		{
			
			var lastAuthenticationEmailAddressTried = App.Database.Settings.GetValue<String>("lastAuthenticationEmailAddressTried");
			return await IsAuthenticated(lastAuthenticationEmailAddressTried);

		}

		public async static Task<bool> IsAuthenticated(String emailaddress)
		{

			var result = false;
			var shouldUpdateAuthentication = false;

			var lastSuccessfullAuthenticationResult = App.Database.Settings.GetValue<DateTime>("lastSuccessfullAuthentication");
			var lastAuthenticationEmailAddressTried = App.Database.Settings.GetValue<String>("lastAuthenticationEmailAddressTried");

			// Update authentication after 30 days.
			if (DateTime.Now - lastSuccessfullAuthenticationResult > new TimeSpan(31, 0, 0, 0))
			{
				shouldUpdateAuthentication = true;
			}

			// Update authentication if emailaddress is different from last succes login
			if (String.IsNullOrEmpty(lastAuthenticationEmailAddressTried) || lastAuthenticationEmailAddressTried != emailaddress)
			{
				shouldUpdateAuthentication = true;
			}



			if (shouldUpdateAuthentication)
			{
				var authResult = await GetAuthenticationResult(emailaddress);
				if (authResult != null)
				{
					App.Database.Settings.Set("lastAuthenticationResult", authResult);
					App.Database.Settings.Set("lastAuthenticationEmailAddressTried", emailaddress);
					if (authResult.Code == AuthorizationCode.Authorized)
					{
						App.Database.Settings.Set("lastSuccessfullAuthentication", DateTime.Now);
						Contact authenticatedContact = App.Database.Contacts.GetAll().Find(x => x.Email1 == emailaddress);
						if (authenticatedContact != null)
						{
							App.Database.Settings.Set("authenticatedContactId", authenticatedContact.Id);
						}
						result = true;
					}
				}
			}
			else
			{
				result = true;
			}

			return result;
		}

		public static Contact GetAuthenticatedContact()
		{
			Int32 contactId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");
			Contact authenticatedContact = App.Database.Contacts.GetById(contactId);
			return authenticatedContact;
		}
	}
}
