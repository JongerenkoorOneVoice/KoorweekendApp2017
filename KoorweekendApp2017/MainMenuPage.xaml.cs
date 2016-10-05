using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class MainMenuPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MainMenuPage()
		{
			InitializeComponent();

			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "HomePage",
				IconSource = "contacts.png",
				TargetType = typeof(HomePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "MainMenuPage",
				IconSource = "todo.png",
				TargetType = typeof(HomePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "ContactPage",
				IconSource = "reminders.png",
				TargetType = typeof(ContactPage)
			});

			listView.ItemsSource = masterPageItems;


		}
	}

	public class MasterPageItem
	{
		public string Title { get; set;}
		public string IconSource { get; set;}
		public Type TargetType { get; set;}

	}
}
