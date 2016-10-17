using KoorweekendApp2017.BusinessObjects;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages;
using SQLite;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class App : Application
	{

		//public DeviceData CurrentDevice = new DeviceData();

		public static  LocalDatabase Database { get; set; }

		public App()
		{
			InitializeComponent();
			MainPage = new KoorweekendApp2017Page();

			var db = DependencyService.Get<ISQLite>().GetConnection();
			Database = new LocalDatabase(db);
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
