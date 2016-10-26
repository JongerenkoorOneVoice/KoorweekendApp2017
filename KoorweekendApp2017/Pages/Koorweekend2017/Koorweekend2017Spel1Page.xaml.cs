﻿using System;
using System.Collections.Generic;
using CocosSharp;
using KoorweekendApp2017.Scenes;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class Koorweekend2017Spel1Page : ContentPage
	{
		Game1Scene1 game1Scene1 {get; set;}

		public Koorweekend2017Spel1Page()
		{
			InitializeComponent();
			// This is the top-level grid, which will split our page in half
			var grid = new Grid();
			this.Content = grid;
			grid.RowDefinitions = new RowDefinitionCollection {
	            // Each half will be the same size:
	            new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
				new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
			};
			CreateTopHalf(grid);
			CreateBottomHalf(grid);
		}

		void CreateTopHalf(Grid grid)
		{
			var gameView = new CocosSharpView()
			{
				// Notice it has the same properties as other XamarinForms Views
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
               	ViewCreated = OnHandleViewCreated
			};
	
			// We'll add it to the top half (row 0)
			grid.Children.Add(gameView, 0, 0);


		}

		void CreateBottomHalf(Grid grid)
		{
			// We'll use a StackLayout to organize our buttons
			var stackLayout = new StackLayout();
			// The first button will move the circle to the left when it is clicked:
			var moveLeftButton = new Button
			{
				Text = "Move Circle Left"
			};
			moveLeftButton.Clicked += (sender, e) => game1Scene1.MoveCircleLeft();
			stackLayout.Children.Add(moveLeftButton);

			// The second button will move the circle to the right when clicked:
			var moveCircleRight = new Button
			{
				Text = "Move Circle Right"
			};
			moveCircleRight.Clicked += (sender, e) => game1Scene1.MoveCircleRight();
			stackLayout.Children.Add(moveCircleRight);
			// The stack layout will be in the bottom half (row 1):

			grid.Children.Add(stackLayout, 0, 1);
		}

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
