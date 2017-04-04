using System;
using System.Collections.Generic;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Pages;
using Xamarin.Forms;
using Plugin.DeviceInfo;
using KoorweekendApp2017.Enums;
using KoorweekendApp2017.Models;
using System.Linq;
using System.Threading.Tasks;
using KoorweekendApp2017.Tasks;
using KoorweekendApp2017.Extensions;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace KoorweekendApp2017
{
	public partial class LoginPage : ContentPage
	{

		public LoginPageIds CurrentPage = LoginPageIds.None;

		public List<LoginPageItem> Pages = new List<LoginPageItem>();

		public LoginPage()
		{
			InitializeComponent();
			page.IsVisible = false;
            SetupPages();
            LoadPage(LoginPageIds.EnterEmail).ConfigureAwait(false);
            pageButton.Clicked += Authenticate;
            

		}

		public async void Authenticate(object s, EventArgs e)
		{
			if (CurrentPage == LoginPageIds.EnterEmail)
			{
               /* var network = Resolver.Resolve<INetwork>();
                if (network.InternetConnectionStatus() != NetworkStatus.NotReachable)
                {*/
                    await RunLoginScript();
               /*  }
               else
                {
                    await DisplayAlert("Geen internet", "Je kan alleen inloggen als je internet aan hebt staan.", "Terug");
                }*/

			}
			else if (CurrentPage == LoginPageIds.EmailNotInList)
			{
				await LoadPage(LoginPageIds.EnterEmail);
			}
			else if (CurrentPage == LoginPageIds.InvalidEmail)
			{
				await LoadPage(LoginPageIds.EnterEmail);
			}
			else if (CurrentPage == LoginPageIds.MailSend)
			{

				var isAuthenticated = await RunLoginScript(shouldSendMail: false);
				if (isAuthenticated)
				{
					Application.Current.MainPage = new KoorweekendApp2017Page();
				}
			}
			//await LoadPage(CurrentPage + 1);

		}

		public LoginPageItem GetPageItem(LoginPageIds pageId)
		{
			return Pages.Find(x => x.PageId == pageId);
		}

		public async Task LoadPage(LoginPageIds pageId, bool animate = true)
		{



			if (animate)
			{
				if (pageId >= CurrentPage)
				{
					await page.TranslateTo(-Math.Abs(page.Width), 0, 500, Easing.CubicIn);
				}
				else {
					await page.TranslateTo(Math.Abs(page.Width), 0, 500, Easing.CubicIn);
				}
				page.IsVisible = false;
			}

			LoginPageItem pageToLoad = GetPageItem(pageId);
			pageButton.Text = pageToLoad.ButtonText;
			pageText.Text = pageToLoad.PageText;
			pageTitle.Text = pageToLoad.PageTitle;
			pageMailInput.IsVisible = pageToLoad.InputIsVisible;
			if (animate )
			{
				if (pageId >= CurrentPage)
				{
					page.TranslationX = Math.Abs(page.Width);
				}
				else
				{
					page.TranslationX = -Math.Abs(page.Width);
				}
				page.IsVisible = true;
				await page.TranslateTo(0, 0, 500, Easing.CubicOut);
			}
			CurrentPage = pageId;

		}
			

		public async Task<bool> RunLoginScript(bool shouldSendMail = true)
		{
			Boolean result = false;
			String emailaddress = pageMailInput.Text;
			if (!emailaddress.IsValidEmailAddres())
			{
				await LoadPage(LoginPageIds.InvalidEmail);
				//await DisplayAlert("Emailadres ongeldig", "Waarschijnlijk heb je je mailadres verkeerd geschreven", "Probeer opnieuw");
				return result;
			}

			var authResult = await AuthenticationHelper.GetAuthenticationResult(emailaddress);
			if (authResult.Code != AuthorizationCode.Authorized)
			{
				if ((authResult.Code != AuthorizationCode.EmailAddressNotFound
				&& authResult.Code != AuthorizationCode.InvalidEmailaddress)
				|| authResult.Code == AuthorizationCode.NotAuthorized)
				{

					await page.TranslateTo(-Math.Abs(page.Width), 0, 500, Easing.CubicIn);
					var mailSend = await AuthenticationHelper.RegisterDevice(emailaddress);
					if (mailSend && shouldSendMail)
					{
						page.TranslationX = Math.Abs(page.Width);
						await page.TranslateTo(0, 0, 500, Easing.CubicOut);
						await LoadPage(LoginPageIds.MailSend, false);
						// do something here;

						return result;
					}
					else
					{
						
						await page.TranslateTo(0, 0, 500, Easing.CubicOut);
					}

					// maybe do somtehting here?
					return result;
				}
				else if (authResult.Code == AuthorizationCode.EmailAddressNotFound)
				{
					await LoadPage(LoginPageIds.EmailNotInList);
					// do something;
				}
			}
			else if (authResult.Code == AuthorizationCode.Authorized)
			{
				result = true;
				App.Database.Settings.Set("lastSuccessfullAuthentication", DateTime.Now);
				App.Database.Settings.Set("lastAuthenticationResult", authResult);
				App.Database.Settings.Set("lastAuthenticationEmailAddressTried", emailaddress);

                DataSync.RunAllTasksAndWaitForReady(true);
				AuthenticationHelper.GetAndWriteCurrentAuthenticatedUserIdToDb();
                
                Application.Current.MainPage = new KoorweekendApp2017Page();
            }
			return result;
		}

		public void SetupPages()
		{
			Pages = new List<LoginPageItem>()
			{
				new LoginPageItem(){
					PageTitle = "Emailadres ongeldig",
					PageText = "Waarschijnlijk heb je je emailadres verkeerd geschreven",
					ButtonText = "Probeer opnieuw",
					PageId = LoginPageIds.InvalidEmail,
					InputIsVisible = false
				},
				new LoginPageItem(){
					PageTitle = "Emailadres afgekeurd",
					PageText = "Het emailadres dat je hebt opgegeven is niet bij ons bekend als het emailadres van een van onze koorleden. Weet je zeker dat het emailadres klopt?",
					ButtonText = "Opnieuw proberen",
					PageId = LoginPageIds.EmailNotInList,
					InputIsVisible = false
				},
				new LoginPageItem(){
					PageTitle = "Aanmelden",
					PageText = "Deze app is alleen beschikbaar voor de leden van Jongerenkoor One Voice.\r\nVul hieronder je emailadres in zodat we kunnen controleren dat je lid bent.",
					ButtonText = "Verder",
					PageId = LoginPageIds.EnterEmail,
					InputIsVisible = true
				},
				new LoginPageItem(){
					PageTitle = "Email verzonden",
					PageText = "We hebben een email gestuurd naar het mailadres dat je hebt opgegeven. Voor je verder kan met inloggen moet je eerst op de link in die mail klikken.",
					ButtonText = "Verder",
					PageId = LoginPageIds.MailSend,
					InputIsVisible = false
				}
			};
		}

	}
}
