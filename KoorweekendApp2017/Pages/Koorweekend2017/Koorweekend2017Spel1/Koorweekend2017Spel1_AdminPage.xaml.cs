using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class Koorweekend2017Spel1_AdminPage : ContentPage
	{
		public Koorweekend2017Spel1_AdminPage()
		{
			InitializeComponent();


			backButton.Clicked +=  BackButtonClicked ;
		}

		void BackButtonClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}
