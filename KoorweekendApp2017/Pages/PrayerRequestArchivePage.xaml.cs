using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Messages;
using KoorweekendApp2017.Models;
using KoorweekendApp2017.Pages;
using KoorweekendApp2017.Tasks;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class PrayerRequestArchivePage : ContentPage
	{

		public List<PrayerRequest> PrayerRequests = new List<PrayerRequest>();

		public Contact CurrentUser { get; set; }

		public Boolean IsEditing { get; set; }

		public PrayerRequest EditingRequest { get; set; }

		public PrayerRequestArchivePage()
		{
			InitializeComponent();


			SetupDataForPrayerRequests();

			Int32 currentUserId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");
			CurrentUser = App.Database.Contacts.GetById(currentUserId);

			prayerRequestListView.ItemSelected += OnPrayerRequestSelected;
			prayerRequestListView.IsPullToRefreshEnabled = true;
			prayerRequestListView.Refreshing += SyncPrayerRequestsWithWebservice;

			ToolbarItems.Add(new ToolbarItem("Add", "Plus.png", () =>
			{
				EditingRequest = new PrayerRequest();
				EditingRequest.ContactId = CurrentUser.Id;
				IsEditing = true;
				Navigation.PushAsync(new PrayerRequestEditPage() { BindingContext = EditingRequest });

			}));



		}

		private void SetupDataForPrayerRequests()
		{
			PrayerRequests = App.Database.PrayerRequests.GetAll();
			PrayerRequests = PrayerRequests.FindAll(x => x.IsVisible != false).ToList();
			PrayerRequests = PrayerRequests.FindAll(x => x.EndDate >= DateTime.Now.AddDays(1).Date).ToList();
			PrayerRequests = PrayerRequests.OrderByDescending(x => x.LastModifiedInApp).ThenByDescending(x => x.LastModifiedInApi).ToList();
			prayerRequestListView.ItemsSource = PrayerRequests;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (IsEditing)
			{
				bool shouldUpdate = true;
				var isValid = true;
				if (String.IsNullOrEmpty(EditingRequest.Title)) isValid = false;
				if (String.IsNullOrEmpty(EditingRequest.Text)) isValid = false;

				if (EditingRequest.AppSpecificId == null && EditingRequest.IsVisible == false)
				{
					shouldUpdate = false;
					isValid = true;
				}

				if (EditingRequest.AppSpecificId != null && EditingRequest.IsVisible == false)
				{
					shouldUpdate = true;
					isValid = true;
				}

				if (!isValid)
				{
					var page = new PrayerRequestEditPage() { BindingContext = EditingRequest };
					page.PageIsValid = false;
					Navigation.PushAsync(page);
				}
				else
				{
					
					if (EditingRequest.AppSpecificId != null)
					{
						var tmp = App.Database.PrayerRequests.GetById(Convert.ToInt32(EditingRequest.AppSpecificId));
						if (tmp.Title == EditingRequest.Title
					   	&& tmp.Text == EditingRequest.Text
						&& tmp.IsVisible == EditingRequest.IsVisible
						&& tmp.IsPrivate == EditingRequest.IsPrivate
					    && tmp.IsAnonymous == EditingRequest.IsAnonymous
					   	&& tmp.IsVisible == EditingRequest.IsVisible) {
							shouldUpdate = false;
						}
					}

					if (shouldUpdate)
					{
						App.Database.PrayerRequests.UpdateOrInsert(EditingRequest);
						SetupDataForPrayerRequests();
						MessagingCenter.Send(new StartApiPrayerRequestSyncMessage(), "StartApiPrayerRequestSyncMessage");
					}
					IsEditing = false;
					EditingRequest = null;
				}

			}
		}

		void OnPrayerRequestSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as PrayerRequest;
			if (item != null)
			{
				if (item.ContactId == CurrentUser.Id)
				{
					EditingRequest = item;
					IsEditing = true;
					Navigation.PushAsync(new PrayerRequestEditPage(){ BindingContext = item });
				}
				else {

					Navigation.PushAsync(new PrayerRequestSinglePage() { BindingContext = item });
				}

				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				prayerRequestListView.SelectedItem = null;

				//IsPresented = false;
			}
		}

		void SyncPrayerRequestsWithWebservice(object sender, EventArgs args)
		{
			MessagingCenter.Send(new StartApiPrayerRequestSyncMessage(), "StartApiPrayerRequestSyncMessage");
			SetupDataForPrayerRequests();
			ListView listView = sender as ListView;
			listView.EndRefresh();
		}


	}
}
