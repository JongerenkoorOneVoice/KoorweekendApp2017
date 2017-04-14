using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoorweekendApp2017.BusinessObjects;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Messages;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages;
using KoorweekendApp2017.Tasks;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using SQLite;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class App : Application
	{

		//public DeviceData CurrentDevice = new DeviceData();

		public static String SettingsScreenUri { get; set; }

		public static LocalDatabase Database { get; set; }

		public static AppWebService AppWebService { get; set; }

		public static IConnectivity Network { get; set; }

		public static Contact CurrentUser { get; set; }

		public App(string uri)
		{
			InitializeComponent();
			var db = DependencyService.Get<ISQLite>().GetConnection();
			Database = new LocalDatabase(db);
			AppWebService = new AppWebService();
			Network = CrossConnectivity.Current;
			SettingsScreenUri = uri;

			if (GlobalSettingsHelper.VersionBiggerThanAppVersion(GlobalSettings.MinimumVersionToAllowRunning))
			{
				// To be sure data is correct, try to update data from web api and check again
				DataSync.UpdateGlobalSettingsInDbFromApi(shouldUpdateAll: true, showAlert: false);
				if (GlobalSettingsHelper.VersionBiggerThanAppVersion(GlobalSettings.MinimumVersionToAllowRunning))
				{
					// App should be updated before loading can continue;
					MainPage = new ToLowAppVersion();
				}
				else
				{
					Load();
				}
			}
			else
			{
				Load();

			}
		}

		public void Load()
		{
			var authTask = AuthenticationHelper.IsAuthenticated();
			authTask.ConfigureAwait(false);

			var isAuthenticated = authTask.Result;

			if (!isAuthenticated)
			{
				MainPage = new LoginPage();
			}
			else
			{
				Int32 currentUserId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");
				CurrentUser = App.Database.Contacts.GetById(currentUserId);

				if (GlobalSettingsHelper.ShouldShowLatesReleaseNotes())
				{
					MainPage = new UpdateOverview();
				}
				else
				{
					MainPage = new KoorweekendApp2017Page();
				}

			}

			DataSync.RunAllTasksInBackground();
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
