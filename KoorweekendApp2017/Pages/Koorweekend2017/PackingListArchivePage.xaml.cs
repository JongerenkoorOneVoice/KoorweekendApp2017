using System;
using System.Collections.Generic;
using XLabs.Forms.Controls;

using Xamarin.Forms;
using KoorweekendApp2017.Models;

namespace KoorweekendApp2017.Pages.Koorweekend2017
{
	public partial class PackingListArchivePage : ContentPage
	{
		public PackingListArchivePage()
		{
			InitializeComponent();
			packingListView.ItemTapped += PackingListView_ItemTapped;

		}

		void PackingListView_ItemTapped(object sender,  ItemTappedEventArgs e)
		{
			var item = e.Item as ChoirWeekendPackingListItem;
			//var list = sender as ListView;

			item.IsPacked = !item.IsPacked;

			App.Database.ChoirWeekend2017.PackingList.UpdateOrInsert(item);
			LoadItems();

		}

		void LoadItems()
		{
			var items = App.Database.ChoirWeekend2017.PackingList.GetAll();
			packingListView.ItemsSource = items;
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			LoadItems();


		}
	}
}
