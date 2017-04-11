using System;
using System.Collections.Generic;
using KoorweekendApp2017.Helpers;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class Koorweekend2017Page : ContentPage
	{

		public bool TimerRunning { get; set; }

		public DateTime CountDowntargetGame1 { get; set; }

		public DateTime CountDowntargetGame2 { get; set; }

		public Koorweekend2017Page()
		{
			InitializeComponent();

			game1Button.IsEnabled = false;
			game1Button.Clicked += game1Button_Clicked;

			game2Button.IsEnabled = false;
			game2Button.Clicked += game2Button_Clicked;

			packinglistButton.Clicked += packinglistButton_Clicked;

			CountDowntargetGame1 = new DateTime(year: 2017, month: 4, day: 21, hour: 21, minute: 0, second: 0);
			CountDowntargetGame2 = new DateTime(year: 2017, month: 4, day: 22, hour: 11, minute: 00, second: 0);
            UpdateCountDowns();
            StartTimer();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			StartTimer();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			StopTimer();
		}

		public void StartTimer()
		{
			if (!TimerRunning)
			{
				TimerRunning = true;
				Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					if (!TimerRunning) return false;

					UpdateCountDowns();

					return true;
				});
			}
		}

		public void UpdateCountDowns()
		{
			var now = DateTime.Now;

			var game1Countdown = CountDowntargetGame1 - now;

			if (game1Countdown.TotalMilliseconds <= 0 || UserHelper.CurrentUserIsDeveloper())
			{
				game1Button.IsEnabled = true;
				game1Button.Text = "Spel 1";
			}
			else
			{
				game1Button.Text = String.Format("Spel 1\r\n{0}", game1Countdown.ToString(@"dd\:hh\:mm\:ss"));
			}

			var game2Countdown = CountDowntargetGame2 - now;
			if (game2Countdown.TotalMilliseconds <= 0 || UserHelper.CurrentUserIsDeveloper())
			{
				game2Button.IsEnabled = true;
				game2Button.Text = "Spel 2";
			}
			else
			{
				game2Button.Text = String.Format("Spel 2\r\n{0}", game2Countdown.ToString(@"dd\:hh\:mm\:ss"));
			}

			if ((game1Countdown.TotalMilliseconds <= 0 && game2Countdown.TotalMilliseconds <= 0)
		    || (UserHelper.CurrentUserIsDeveloper()))
			{
				StopTimer();
			}
		}

		public void StopTimer()
		{
			TimerRunning = false;
		}

		void game1Button_Clicked(object sender, EventArgs e)
		{
			try
			{
				this.Navigation.PushAsync(new Koorweekend2017Spel1Page());
			}
			catch (Exception ex)
			{
				var a = ex;
			}
		}

		void game2Button_Clicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new Koorweekend2017Spel2.Koorweekend2017Spel2Page());
		}

		void packinglistButton_Clicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new PackingListArchivePage());
		}
	}
}
