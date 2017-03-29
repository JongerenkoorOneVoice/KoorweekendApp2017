using System;
using System.Collections.Generic;


using Xamarin.Forms;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class Koorweekend2017Page : ContentPage
	{
		public Koorweekend2017Page()
		{
			InitializeComponent();
			game1Button.Clicked += game1Button_Clicked;
			game2Button.Clicked += game2Button_Clicked;
			packinglistButton.Clicked += packinglistButton_Clicked;

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
