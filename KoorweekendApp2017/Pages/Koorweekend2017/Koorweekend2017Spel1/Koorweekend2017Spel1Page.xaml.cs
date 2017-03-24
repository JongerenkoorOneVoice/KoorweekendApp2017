using System;
using System.Collections.Generic;
using CocosSharp;
using KoorweekendApp2017.Scenes;
using Plugin.Compass;
using Xamarin.Forms;

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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			CrossCompass.Current.Start();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			CrossCompass.Current.Stop();
		}
	}
}
