using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoorweekendApp2017.Enums;
using KoorweekendApp2017.Extensions;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public class AuthenticationHelper
	{

		public static Int32 GetAndWriteCurrentAuthenticatedUserIdToDb()
		{
			String emailAddress = App.Database.Settings.GetValue<String>("lastAuthenticationEmailAddressTried");
			AuthenticationResult authenticationResult = App.Database.Settings.GetValue<AuthenticationResult>("lastAuthenticationResult");
			if (authenticationResult != null && authenticationResult.Code == AuthorizationCode.Authorized)
			{
				Contact authenticatedContact = App.Database.Contacts.GetAll().Find(x => x.Email1 == emailAddress);
				if (authenticatedContact != null)
				{
					App.Database.Settings.Set("authenticatedContactId", authenticatedContact.Id);
				}
				return authenticatedContact.Id;
			}
			return 0;

		}

		public static String CreateSecureAuthenticatedUrl(String url)
		{

			// Get current url
			Uri originalUrl = new Uri(url);


			// Get authentication values.
			String emailAddress = App.Database.Settings.GetValue<String>("lastAuthenticationEmailAddressTried");
			String deviceId = new CustomDevice(CrossDeviceInfo.Current).Id;


			String newUrl = url;

			if (originalUrl.Host == "www.jongerenkooronevoice.nl")
			{
				Dictionary<String, String> queryData = new Dictionary<string, string>();
				String query = string.Empty;

				if (!String.IsNullOrEmpty(originalUrl.Query))
				{
					String[] tempQueryDataArray = originalUrl.Query.TrimStart(new char[] { '?' }).Split(new String[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
					List<String> tempQueryData = new List<String>(tempQueryDataArray);


					foreach (String keyValue in tempQueryData)
					{
						String[] keyValueArray = keyValue.Split(new String[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
						List<String> keyValueList = new List<String>(keyValueArray);
						//if (keyValueList.Count != 2)  //throw new Exception("Invalid query in url");
						String key = keyValueList.First();
						String value = keyValueList.Count == 2 ? keyValueList.Last() : "";

						if (key.ToLower() != "emailaddress" && key.ToLower() != "deviceid")
						{
							queryData.Add(key, value);
						}
						else
						{
							if (String.IsNullOrEmpty(emailAddress) && key.ToLower() == "emailaddress")
							{
								if (value.IsValidEmailAddres()) emailAddress = value;
							}
						}
					}
				}

				queryData.Add("EmailAddress", emailAddress);
				queryData.Add("DeviceId", deviceId);

				foreach (KeyValuePair<String, String> item in queryData)
				{
					query += String.Format("&{0}={1}", item.Key, item.Value);
				}

				newUrl = String.Format("https://{0}{1}?{2}", originalUrl.Host, originalUrl.LocalPath, query.TrimStart(new char[] { '&' }));
			}

			return newUrl;
		}

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
				AuthenticationResult authResult = await GetAuthenticationResult(emailaddress);
				if (authResult != null)
				{
					App.Database.Settings.Set("lastAuthenticationResult", authResult);
                    if (!String.IsNullOrEmpty(emailaddress) && emailaddress.IsValidEmailAddres())
                    {
                        App.Database.Settings.Set("lastAuthenticationEmailAddressTried", emailaddress);
                    }
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
