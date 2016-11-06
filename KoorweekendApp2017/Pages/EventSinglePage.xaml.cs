using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class EventSinglePage : ContentPage
	{
		public EventSinglePage()
		{
			InitializeComponent();





		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var currentEvent = BindingContext as Event;
			List<Song> songsForEvent = new List<Song>();



			songForEventListView.ItemsSource = songsForEvent;

		}

	}
}
