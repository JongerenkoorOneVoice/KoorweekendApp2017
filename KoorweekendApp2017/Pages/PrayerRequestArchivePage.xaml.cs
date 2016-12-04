using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages;
using KoorweekendApp2017.Tasks;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class PrayerRequestArchivePage : ContentPage
	{

		public List<PrayerRequest> PrayerRequests = new List<PrayerRequest>();


		public PrayerRequestArchivePage()
		{
			InitializeComponent();

			PrayerRequests = App.Database.PrayerRequests.GetAll();
			PrayerRequests = PrayerRequests.OrderBy(x => x.LastModifiedInApp).ThenBy(x=> x.LastModifiedInApi).ToList();
			prayerRequestListView.ItemsSource = PrayerRequests;

			prayerRequestListView.ItemSelected += OnPrayerRequestSelected;
			prayerRequestListView.IsPullToRefreshEnabled = true;
			prayerRequestListView.Refreshing += SyncPrayerRequestsWithWebservice;

			ToolbarItems.Add(new ToolbarItem("Add", "filter_25.png", () =>
			{
				Navigation.PushAsync(new PrayerRequestEditPage());
			}));

			// Remove after testing
			foreach (var item in PrayerRequests)
			{
				App.Database.PrayerRequests.RemoveById(Convert.ToInt32(item.AppSpecificId));
			}

		}

		void OnPrayerRequestSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as PrayerRequest;
			if (item != null)
			{
				Navigation.PushAsync(new PrayerRequestSinglePage() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				prayerRequestListView.SelectedItem = null;

				//IsPresented = false;
			}
		}

		void SyncPrayerRequestsWithWebservice(object sender, EventArgs args)
		{
			DataSync.SyncPrayerRequests(true);
			ListView listView = sender as ListView;
			listView.ItemsSource = App.Database.PrayerRequests.GetAll();
			listView.EndRefresh();
		}


	}
}
