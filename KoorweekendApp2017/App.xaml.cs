﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoorweekendApp2017.BusinessObjects;
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

		public static LocalDatabase Database { get; set; }

		public static AppWebService AppWebService { get; set; }

		public static IConnectivity Network { get; set; }

		public static Contact CurrentUser { get; set; }

		public App()
		{
			InitializeComponent();
			var db = DependencyService.Get<ISQLite>().GetConnection();
			Database = new LocalDatabase(db);
			AppWebService = new AppWebService();
			Network = CrossConnectivity.Current;
			//DataSync.UpdateContactsInDbFromApi();
			//DataSync.UpdateSongsInDbFromApi();
			//DataSync.UpdateEventsInDbFromApi();

			//DataSync.UpdateGame1AssignmentsInDbFromApi(true);
			//DataSync.UpdateGame2AssignmentsInDbFromApi(true);
			//DataSync.UpdatePackinglistInDbFromApi(true);

			DataSync.RunAllTasksInBackground();
			//DataSync.RunAllTasksAndWaitForReady();
			//MessagingCenter.Send(new StopApiContactSyncMessage(), "StopApiContactSyncMessage");

			//var authTask = AuthenticationHelper.IsAuthenticated();
			//authTask.RunSynchronously();
			Boolean? forceLogin = App.Database.Settings.GetValue<Boolean?>("loginOnNextStart");
			if (forceLogin != null && forceLogin == true)
			{
				App.Database.Settings.RemoveByKey("lastSuccessfullAuthentication");
				App.Database.Settings.RemoveByKey("lastAuthenticationResult");
				App.Database.Settings.RemoveByKey("lastAuthenticationEmailAddressTried");
				App.Database.Settings.Set("loginOnNextStart", false);
			}

			var isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (!isAuthenticated)
			{
				MainPage = new LoginPage();
			}
			else {
				Int32 currentUserId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");
				CurrentUser = App.Database.Contacts.GetById(currentUserId);

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
