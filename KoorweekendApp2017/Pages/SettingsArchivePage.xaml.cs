using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class SettingsArchivePage : ContentPage
	{
		public SettingsArchivePage()
		{
			InitializeComponent();
			settingListView.ItemsSource = App.Database.Settings.GetAll();


		}
	}
}
