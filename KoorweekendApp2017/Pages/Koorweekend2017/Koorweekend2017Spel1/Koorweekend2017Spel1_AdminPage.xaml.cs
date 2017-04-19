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

		async void GameResetButtonClicked(object sender, EventArgs e)
		{
			var reset = await Application.Current.MainPage.DisplayAlert("Zeker weten?", "Als je doorgaat worden alle scores gereset.\r\nWeet je zeker dat je door wilt gaan?", "Ja, reset", "Nee, toch niet");
			if (reset)
			{
				App.Database.Settings.RemoveByKey("2017game1PenaltyPoints");
				var assignmentList = App.Database.ChoirWeekend2017.Game1.GetAll();
				foreach (var assignment in assignmentList)
				{
					assignment.Result = new ChoirWeekendBaseAssignmentResult();
					App.Database.ChoirWeekend2017.Game1.UpdateOrInsert(assignment);
				}
				TimerResetButton(sender, e);

			}
		}

		async void TimerResetButton(object sender, EventArgs e)
		{
			App.Database.Settings.RemoveByKey("2017Game1StartedAt");
			Navigation.PopAsync();
			Navigation.PopAsync();
		}
	}
}
