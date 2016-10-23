using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class LogArchivePage : ContentPage
	{
		public LogArchivePage()
		{
			try
			{
				InitializeComponent();
				logListView.ItemSelected += OnEventSelected;
				List<LogItem> logsItems = App.Database.LogItems.GetAll();
				logsItems = logsItems.OrderBy(e => e.Time).ToList();
				logListView.ItemsSource = logsItems;


			}
			catch (Exception ex)
			{
				var a = ex.Message;
			}
		}

		void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as LogItem;
			if (item != null)
			{
				Navigation.PushAsync(new LogSinglePage() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				logListView.SelectedItem = null;

				//IsPresented = false;
			}
		}
	}
}
