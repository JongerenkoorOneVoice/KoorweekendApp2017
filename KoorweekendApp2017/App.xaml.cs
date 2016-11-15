using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoorweekendApp2017.BusinessObjects;
using KoorweekendApp2017.Messages;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages;
using KoorweekendApp2017.Tasks;
using SQLite;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class App : Application
	{

		//public DeviceData CurrentDevice = new DeviceData();

		public static LocalDatabase Database { get; set; }

		public App()
		{
			InitializeComponent();
			var db = DependencyService.Get<ISQLite>().GetConnection();
			Database = new LocalDatabase(db);
			//DataSync.UpdateContactsInDbFromApi();
			//DataSync.UpdateSongsInDbFromApi();
			//DataSync.UpdateEventsInDbFromApi();
			MessagingCenter.Send(new StartApiContactSyncMessage(), "StartApiContactSyncMessage");
			MessagingCenter.Send(new StartApiSongSyncMessage(), "StartApiSongSyncMessage");
			MessagingCenter.Send(new StartApiEventSyncMessage(), "StartApiEventSyncMessage");


			//MessagingCenter.Send(new StopApiContactSyncMessage(), "StopApiContactSyncMessage");

			//var authTask = AuthenticationHelper.IsAuthenticated();
			//authTask.RunSynchronously();
			//App.Database.Settings.RemoveByKey("lastSuccessfullAuthentication");
			//App.Database.Settings.RemoveByKey("lastAuthenticationResult");
			//App.Database.Settings.RemoveByKey("lastAuthenticationEmailAddressTried");
			var isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (!isAuthenticated)
			{
				MainPage = new LoginPage();
			}
			else {
				MainPage = new KoorweekendApp2017Page();
			}



		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}


	}
}
