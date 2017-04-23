using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace KoorweekendApp2017
{
	public partial class UpdateOverview : ContentPage
	{
		public KoorweekendApp2017Page mainAppPage { get; set; }

		public UpdateOverview()
		{
			InitializeComponent();
			updateView.Source = GetHTML();
			var device = Resolver.Resolve<IDevice>();
			updateView.HeightRequest = device.Display.Height - okButton.Height;
			okButton.Clicked += OkButtonClicked;
			Task.Run(() => { 
				mainAppPage = new KoorweekendApp2017Page();
			});

		}

	

		private HtmlWebViewSource GetHTML()
		{
			HtmlWebViewSource document = new HtmlWebViewSource();
			var setting = App.Database.GlobalSettings.GetByKey("currentVersionReleaseNotes");
			if (setting != null)
			{

				document.Html = String.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", "", setting.Value);

			}
			else
			{
				document.Html = "Geen gegevens beschikbaar.";
			}


			return document;
		}

		void OkButtonClicked(object sender, EventArgs e)
		{
			App.Database.Settings.Set("releasenotesLastShownForVersion", HardAppSettings.Version);
			if (mainAppPage == null)
			{
				mainAppPage = new KoorweekendApp2017Page();
			}
			Application.Current.MainPage = mainAppPage;

		}

	}
}
