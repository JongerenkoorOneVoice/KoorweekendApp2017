using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class Koorweekend2017Spel1_AdminPage : ContentPage
	{
		public Koorweekend2017Spel1_AdminPage()
		{
			InitializeComponent();

			backButton.IsVisible = false;
			//backButton.Clicked +=  BackButtonClicked ;

			resetGameButton.Clicked += GameResetButtonClicked;

			resetTimerButton.Clicked += TimerResetButton;

		}

		void BackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		void GameResetButtonClicked(object sender, EventArgs e)
		{
			var assignmentList = App.Database.ChoirWeekend2017.Game1.GetAll();
			foreach (var assignment in assignmentList)
			{
				assignment.Result = new ChoirWeekendBaseAssignmentResult();
				App.Database.ChoirWeekend2017.Game1.UpdateOrInsert(assignment);
			}
			Navigation.PopAsync();
			Navigation.PopAsync();

		}

		void TimerResetButton(object sender, EventArgs e)
		{
			App.Database.Settings.RemoveByKey("2017Game1StartedAt");
		}
	}
}
