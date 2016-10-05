using System;
using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class KoorweekendApp2017Page : MasterDetailPage
	{
		public KoorweekendApp2017Page()
		{
			InitializeComponent();
			menuPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				Detail = new MainNavigationPage((Page)Activator.CreateInstance(item.TargetType));
				menuPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
