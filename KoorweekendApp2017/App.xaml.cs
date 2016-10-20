using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoorweekendApp2017.BusinessObjects;
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
			DataSync.UpdateContactsInDbFromApi();
			DataSync.UpdateSongsInDbFromApi();
			DataSync.UpdateEventsInDbFromApi();

			MainPage = new KoorweekendApp2017Page();

			//var x = Database.Settings.

			/*
			Contact x = new Contact();
			x.Id = 999;
			x.Address = "Boezemkade 51";
			x.AreaCode = "2987BC";
			x.BirthDate = "18-12-1982";
			x.Email1 = "mail@marcvannieuwenhuijzen.nl";
			x.FirstName = "Marc";
			x.LastName = "van Nieuwenhuijzen";
			x.Mobile1 = "0641143704";
			x.Phone1 = "000000000";
			x.StartDate = "12-12-1912";
			x.City = "Ridderkerk";
			Database.InsertContact(x);
			var a = Database.GetItems();
			var b = a;
			*/

			//Setting setting = Database.GetSettingByKey("lastContactUpdate");
			//String value = Database.Settings.GetByKey("lastContactUpdate").Value;

			//if (setting == null)
			//{
			/*
				Database.InsertSetting(
					new Setting()
					{
						Key = "lastContactUpdate",
						Value = DateTime.Now.ToString()
					}
			  	);

				Database.InsertSetting(
					new Setting()
					{
						Key = "lastContactUpdate",
						Value = DateTime.Now.AddDays(1).ToString()
					}
			  	);

*/
			//}

			    //Dictionary<String, Setting> settings = Database.GetAllSettings();
			//var x = settings;    

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
