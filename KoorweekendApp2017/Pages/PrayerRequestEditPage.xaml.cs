using System;
using System.Collections.Generic;
using System.ComponentModel;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class PrayerRequestEditPage : ContentPage
	{

		public Boolean PageIsValid = true;

		public PrayerRequest CurrentItem {get; set;}

		public PrayerRequestEditPage()
		{
			InitializeComponent();


			endDateCtrl.Format = "dd-MM-yyyy";

			isVisibleCtrl.Clicked += (sender, e) => {
				CurrentItem.IsVisible = false;
				Navigation.PopAsync();
			};

			saveCtrl.Clicked += (sender, e) =>
			{
				Navigation.PopAsync();
			};
		}

		protected override void OnBindingContextChanged()
		{
			

			CurrentItem = BindingContext as PrayerRequest;
			if (CurrentItem.EndDate == null) CurrentItem.EndDate = DateTime.Now.AddMonths(2);

			if (CurrentItem.AppSpecificId == null)
			{
				isVisibleCtrl.Text = "Annuleren";
			}
			else {
				isVisibleCtrl.Text = "Verwijderen";
			}
			base.OnBindingContextChanged();
		}

		protected override void OnAppearing()
		{
			if (!PageIsValid)
			{
				DisplayAlert("Incompleet", "Je hebt niet alle verplichte velden voor dit gebedsverzoek ingevuld.", "Terug").ConfigureAwait(false);
			}


		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}


	}
}
