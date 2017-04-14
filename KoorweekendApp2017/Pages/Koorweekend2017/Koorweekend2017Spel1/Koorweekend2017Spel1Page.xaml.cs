using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocosSharp;
using KoorweekendApp2017.Scenes;
using Plugin.Compass;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using System.Linq;
using XLabs.Platform.Device;

namespace KoorweekendApp2017
{
	public partial class Koorweekend2017Spel1Page : ContentPage
	{
		public Game1Scene1 game1Scene1 { get; set; }

		public Koorweekend2017Spel1Page()
		{
			InitializeComponent();
			// This is the top-level grid, which will split our page in half
			var stackLayout = new StackLayout();
			Content = stackLayout;



			var gameView = new CocosSharpView()
			{
				// Notice it has the same properties as other XamarinForms Views
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				ViewCreated = OnHandleViewCreated
			};
			//gameView.BackgroundColor = Color.Green;
			// We'll add it to the top half (row 0)
			stackLayout.Children.Add(gameView);

		}

		void OnHandleViewCreated(object sender, EventArgs e)
		{
			var gameView = sender as CCGameView;
			if (gameView != null)
			{
				// This sets the game "world" resolution to 100x100:
				gameView.DesignResolution = new CCSizeI(1000, 1250);
				// GameScene is the root of the CocosSharp rendering hierarchy:
				game1Scene1 = new Game1Scene1(gameView);
				// Starts CocosSharp:
				gameView.RunWithScene(game1Scene1);
			}

		}

		public async Task<Game1Settings> RunChecks(Game1Settings gameSettings)
		{

				if (!gameSettings.CompassIsSupported && !gameSettings.ShouldRunCheckAgain)
				{
					CrossCompass.Current.Start();
					await DisplayAlert("Geen Kompas", "Omdat je geen kompas op je telefoon hebt zal het scherm niet met je bewegingen meedraaien.", "Oké");
				}

			var returnToPreviousPage = false;
			if (gameSettings.GpsAvailable)
			{
				if (gameSettings.GpsEnabled && !gameSettings.ShouldRunCheckAgain)
				{

					if (Device.RuntimePlatform == Device.iOS)
					{
						var loadSettings = await DisplayAlert("Geen GPS", "Omdat je geen GPS op je telefoon hebt zal dit spel op jouw telefoon niet werken.", "Instellingen", "Terug");
						if (loadSettings)
						{
							Device.OpenUri(new Uri(App.SettingsScreenUri));
							gameSettings.ShouldRunCheckAgain = true;
						}
						else
						{
							returnToPreviousPage = true;
						}
					}
					else
					{
						await DisplayAlert("Geen GPS", "Zet eerst je GPS aan om dit spel te kunnen spelen.", "Oké");
						returnToPreviousPage = true;
					}
				}
			}
			else
			{
				await DisplayAlert("Geen GPS", "Omdat je geen GPS op je telefoon hebt zal dit spel op jouw telefoon niet werken.", "Oké");
				returnToPreviousPage = true;
			}

			if (returnToPreviousPage)
			{
				Navigation.PopAsync();
			}
			return gameSettings;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			/*
			var gameSettings = new Game1Settings();
			gameSettings.CompassIsSupported = CrossCompass.Current.IsSupported;
			gameSettings.GpsAvailable = CrossGeolocator.Current.IsGeolocationAvailable;
			gameSettings.GpsEnabled = CrossGeolocator.Current.IsGeolocationEnabled;



			RunChecks(gameSettings).ContinueWith((arg) => {
				var settings = arg.Result as Game1Settings;
				if (gameSettings.ShouldRunCheckAgain)
				{
					RunChecks(gameSettings);
				}

				var x = 1;
			});

*/

			/*



			
*/
			if (CrossCompass.Current.IsSupported)
			{
				CrossCompass.Current.Start();
			}

			var settings = new ListenerSettings();
			settings.ActivityType = ActivityType.Fitness;
			settings.AllowBackgroundUpdates = true;


			CrossGeolocator.Current.DesiredAccuracy = 0.25;
			CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 0, 0 , 200), 0.25, true, settings);

		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		
			if (CrossCompass.Current.IsSupported)
			{
				CrossCompass.Current.Stop();
			}

			CrossGeolocator.Current.StopListeningAsync();
		}
	}
}
