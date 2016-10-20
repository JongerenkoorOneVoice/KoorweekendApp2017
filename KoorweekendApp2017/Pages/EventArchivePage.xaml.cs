using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class EventArchivePage : ContentPage
	{
		public EventArchivePage()
		{
			try
			{
				InitializeComponent();
				eventListView.ItemSelected += OnEventSelected;
				List<Event> events = App.Database.Events.GetAll();
				events = events.OrderBy(e => e.Title).ToList();
				eventListView.ItemsSource = events;


			}
			catch (Exception ex)
			{
				var a = ex.Message;
			}
		}

		void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Event;
			if (item != null)
			{
				Navigation.PushAsync(new EventSinglePage() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				eventListView.SelectedItem = null;

				//IsPresented = false;
			}
		}

	}
}
