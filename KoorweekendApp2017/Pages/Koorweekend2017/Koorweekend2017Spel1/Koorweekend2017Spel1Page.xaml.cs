using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocosSharp;
using KoorweekendApp2017.Scenes;
using Plugin.Compass;
//using Plugin.Geolocator;
//using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using System.Linq;
using XLabs.Platform.Device;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;
using KoorweekendApp2017.BusinessObjects;
using KoorweekendApp2017.Enums;
using Acr.UserDialogs;
using KoorweekendApp2017.Helpers;
using Plugin.Connectivity.Abstractions;

namespace KoorweekendApp2017
{
	public partial class Koorweekend2017Spel1Page : ContentPage
	{
		public bool TimerRunning { get; set; }

		public Game1Scene1 game1Scene1 { get; set; }

		private Timer _oneSecondInterval { get; set; }

		private DateTime _gameStartedAt { get; set; }

		private bool _gameOverAlertShown { get; set; } = false;

		public Koorweekend2017Spel1Page()
		{
			InitializeComponent();

			_oneSecondInterval = new Timer(TimeSpan.FromSeconds(1), OneSecondIntervalHandler, TimerType.Interval);


			// This is the top-level grid, which will split our page in half
			var stackLayout = new StackLayout();
			Content = stackLayout;

			ToolbarItems.Add(new ToolbarItem("Get GPS", "getlocation.png", async () => {
				await GetCurrentLocation();
			}));

			ToolbarItems.Add(new ToolbarItem("Manage game", "gear_white.png", () => {
				RunActionOnMainThreadAfterLogin(() => {
					Navigation.PushAsync(new Koorweekend2017Spel1_AdminPage());
                 });
			}));

			ToolbarItems.Add(new ToolbarItem("Game rules", "score_cup.png", () => {
				 Navigation.PushAsync(new Koorweekend2017Spel1_ScorePage());
			}));

			ToolbarItems.Add(new ToolbarItem("View score", "game_rules_list.png", () => {
				Navigation.PushAsync(new Koorweekend2017Spel1_DescriptionPage());
			}));

			var gameView = new CocosSharpView()
			{
				// Notice it has the same properties as other XamarinForms Views
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				ResolutionPolicy = CocosSharpView.ViewResolutionPolicy.ShowAll,
				DesignResolution = new Size(1000, 1250),
				ViewCreated = OnHandleViewCreated
					
			};

			//gameView.BackgroundColor = Color.Green;
			// We'll add it to the top half (row 0)
			stackLayout.Children.Add(gameView);



		}

		void RunActionOnMainThreadAfterLogin(Action action)
		{
			var loginScreen = new LoginConfig();
			loginScreen.Title = "Voeg gebruikesnaam en wachtwoord in";
			loginScreen.OkText = "Oké";
			loginScreen.CancelText = "Annuleren";
			loginScreen.LoginPlaceholder = "Gebruikersnaam";
			loginScreen.PasswordPlaceholder = "Wachtwoord";
			loginScreen.LoginValue = "Beheerder"; 

			var task = UserDialogs.Instance.LoginAsync(loginScreen);
			task.ContinueWith((resultData) => {
				var loginResult = resultData.Result;
				if (loginResult.Ok)
				{
					if (loginResult.Value.UserName == "Beheerder" && loginResult.Value.Password == "KoorweekendEOTB")
					{
						Device.BeginInvokeOnMainThread(() => {
							action.Invoke();
						});
					}
				}
			});
		}

		void OneSecondIntervalHandler()
		{
			UpdateGameTime();
		}

		async void UpdateGameTime()
		{
			var timeToPlay = TimeSpan.FromHours(2);
			var timePlayed = DateTime.Now - _gameStartedAt;
			var timeLeft = timeToPlay - timePlayed;

			if (timeLeft < TimeSpan.FromSeconds(0))
			{
				timeLeft = TimeSpan.FromSeconds(0);
				if (game1Scene1 != null)
				{
					game1Scene1.DataLayer.GameEnded = true;
					if (game1Scene1.DataLayer.GameEnded && !_gameOverAlertShown)
					{
						_gameOverAlertShown = true;
						await DisplayAlert("Game over", "De tijd is voorbij. Alleen het eindpunt blijft zichtbaar", "Oké");

					}
					game1Scene1.DataLayer.UpdateAllLocations();

				}
				
			}

			Title = String.Format("{0}:{1}:{2}", timeLeft.ToString("hh"), timeLeft.ToString("mm"), timeLeft.ToString("ss"));
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

		async Task GetCurrentLocation()
		{
			var position = game1Scene1.DataLayer.CurrentPosition;
			if (position != null)
			{

				var dontShowGoogleMaps = true;
				if (App.Network.IsConnected
				    && (App.Network.ConnectionTypes.Contains(ConnectionType.Cellular) || App.Network.ConnectionTypes.Contains(ConnectionType.WiFi)))
				{
					dontShowGoogleMaps = await DisplayAlert("Huidige positie", String.Format("Longitude: {0}\r\nLatitude: {1}\r\nNauwkeurigheid: {2}", position.Longitude, position.Latitude, position.Accuracy), "Oké", "Google Maps");
				}
				else
				{
					await DisplayAlert("Huidige positie", String.Format("Longitude: {0}\r\nLatitude: {1}\r\nNauwkeurigheid: {2}", position.Longitude, position.Latitude, position.Accuracy), "Oké");
				}

				if (!dontShowGoogleMaps)
				{
					var result2 = await DisplayAlert("Google Maps", "Je lokatie in Google Maps bekijken kost je 1 strafpunt (internet vereist). Wil je doorgaan?", "Ja", "Nee");
					if (result2)
					{
						int penalyPoints = 0;
						var setting = App.Database.Settings.GetByKey("2017game1PenaltyPoints");
						if (setting != null)
						{
							penalyPoints = Convert.ToInt32(setting.Value);
						}

						if (NetworkHelper.IsReachable("google.com"))
						{
							penalyPoints++;
							App.Database.Settings.Set("2017game1PenaltyPoints", penalyPoints);
							Device.OpenUri(new Uri(string.Format("http://maps.google.com/maps?q={0},{1}", position.Latitude, position.Longitude)));
						}
						else
						{
							await DisplayAlert("Mislukt", "Google.com is niet bereikbaar. Je strafpunten zijn niet afgeschreven", "Oké");
						}
					}
				}

			}
			else
			{
				await DisplayAlert("Huidige positie", "Helaas is er nog geen positie beschikbaar" , "Oké");
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

			var gameStartedAt = App.Database.Settings.GetValue<DateTime>("2017Game1StartedAt");
			if (gameStartedAt == DateTime.MinValue)
			{
				_gameStartedAt = DateTime.Now;
				App.Database.Settings.Set("2017Game1StartedAt", _gameStartedAt);
			}
			else
			{
				_gameStartedAt = gameStartedAt;
			}

			_oneSecondInterval.Start();
			var gameSettings = new Game1Settings();
			gameSettings.CompassIsSupported = CrossCompass.Current.IsSupported;
			gameSettings.GpsAvailable = CrossGeolocator.Current.IsGeolocationAvailable;
			gameSettings.GpsEnabled = CrossGeolocator.Current.IsGeolocationEnabled;


			/*
			RunChecks(gameSettings).ContinueWith((arg) => {
				var settings = arg.Result as Game1Settings;
				if (gameSettings.ShouldRunCheckAgain)
				{
					RunChecks(gameSettings);
				}

			});
*/



			if (CrossCompass.Current.IsSupported)
			{
				CrossCompass.Current.Start();
			}

			//var settings = new ListenerSettings();
			//settings.ActivityType = ActivityType.Fitness;
			//settings.AllowBackgroundUpdates = true;


			CrossGeolocator.Current.DesiredAccuracy = 0.25;
			//CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 0, 0 , 200), 0.25, true, settings);
			//CrossGeolocator.Current.StartListeningAsync(200, 0.25, true);
			CrossGeolocator.Current.StartListening(200, 0.25, true);
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_oneSecondInterval.Stop();

			if (CrossCompass.Current.IsSupported)
			{
				CrossCompass.Current.Stop();
			}

			CrossGeolocator.Current.StopListening();
		}
	}
}
