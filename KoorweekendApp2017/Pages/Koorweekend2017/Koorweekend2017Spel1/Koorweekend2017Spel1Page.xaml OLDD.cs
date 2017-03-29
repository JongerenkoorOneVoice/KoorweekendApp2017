using System;
using System.Collections.Generic;
using CocosSharp;
using KoorweekendApp2017.Scenes;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;
using Plugin.Compass;
using Plugin.Compass.Abstractions;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class Koorweekend2017Spel1Page : ContentPage
	{
		Game1Scene1 game1Scene1 {get; set;}

		//private IGeolocator Locator = CrossGeolocator.Current;

		private ListenerSettings LocatorSettings = new ListenerSettings();

		//private ICompass Compass = CrossCompass.Current;

		private Label Latitude = new Label() { Text = "Nog geen data"};

		private Label Longitude = new Label() { Text = "Nog geen data" };

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
			gameView.BackgroundColor = Color.Green;
			// We'll add it to the top half (row 0)
			stackLayout.Children.Add(gameView);


			/*
			Compass.CompassChanged += (s, e) =>
			{
				Longitude.Text = Convert.ToString(e.Heading);
				gameView.BackgroundColor = Color.Green;
				gameView.RotateTo(e.Heading);
			};*/
			/*
			var position1 = Locator.GetPositionAsync(100).ConfigureAwait(false);


			LocatorSettings.AllowBackgroundUpdates = true;

			Locator.DesiredAccuracy = 0.25;
			Locator.PositionChanged += (sender, e) =>
			{
				var position2 = e.Position;

				//Longitude.Text = Convert.ToString(position2.Latitude);
				//Latitude.Text = Convert.ToString(position2);
				//game1Scene1.UpdatePosition(position2);
			};


*/


		}
		/*
		protected override void OnAppearing()
		{
			
			Locator.StartListeningAsync(1, 0.25, false, LocatorSettings);
			Compass.Start();
		}

		protected override void OnDisappearing()
		{
			Locator.StopListeningAsync();
			Compass.Stop();
		}

		void CreateTopHalf(Grid grid)
		{
			

		}

*/

		void OnHandleViewCreated(object sender, EventArgs e)
		{
			var gameView = sender as CCGameView;
			if (gameView != null)
			{
				// This sets the game "world" resolution to 100x100:
				gameView.DesignResolution = new CCSizeI(100, 100);
				// GameScene is the root of the CocosSharp rendering hierarchy:
				game1Scene1 = new Game1Scene1(gameView);
				// Starts CocosSharp:
				gameView.RunWithScene(game1Scene1);
			}

		}
	}
}
