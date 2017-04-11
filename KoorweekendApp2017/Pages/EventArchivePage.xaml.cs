using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Tasks;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class EventArchivePage : ContentPage
	{
		public List<Event> Events = new List<Event>();

		public EventArchivePage()
		{
			try
			{
				InitializeComponent();
				eventListView.ItemSelected += OnEventSelected;
				SettingsHelper.TogglePullToRefresh(eventListView);
				App.Network.ConnectivityTypeChanged += (object sender, ConnectivityTypeChangedEventArgs e) => {
					SettingsHelper.TogglePullToRefresh(eventListView);
				};
				eventListView.Refreshing += ReloadEventsFromWebservice;
				SetupEventDataForList();

			}
			catch (Exception ex)
			{
				var a = ex.Message;
			}
		}

		private void SetupEventDataForList()
		{
			Events = App.Database.Events.GetAll();
			Events = Events.FindAll(x=> x.StartDate >= DateTime.Now.Date).OrderBy(x => x.StartDate).ThenBy(x => x.StartTime).ToList();
			eventListView.ItemsSource = Events;

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

		void ReloadEventsFromWebservice(object sender, EventArgs args)
		{
			DataSync.UpdateEventsInDbFromApi(true);
			SetupEventDataForList();
			ListView listView = sender as ListView;
			listView.EndRefresh();

		}

	}
}
