using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();


			MainPage = new KoorweekendApp2017Page()
			{
				Master = new ContentPage(){
					Content = new Label
					{
						Text = "menu"
					},
					Title = "MasterPage"
				},
				Detail = new NavigationPage( new ContentPage()
				{
					Content = new Label
					{
						Text = "test"
					},
					Title = "DetailPage"
				}),
				Title = "MasterDetail"
			};

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
