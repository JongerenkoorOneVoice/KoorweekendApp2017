using System;
using System.Collections.Generic;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Xamarin.Forms;
using System.Linq;

namespace KoorweekendApp2017.Pages
{
	public partial class ContactArchivePage : ContentPage
	{
		

		public ListView ContactListView { get { return contactListView; } }


		public ContactArchivePage()
		{
			try
			{
				InitializeComponent();
				contactListView.ItemSelected += OnContactSelected;
				List<Contact> contacts = RestHelper.GetRestDataFromUrl<Contact>("http://www.jongerenkooronevoice.nl/contacts/all").Result;
				contacts = contacts.OrderBy(contact => contact.FirstName).ToList();
				contactListView.ItemsSource = contacts;


			}
			catch (Exception ex)
			{
				var a = ex.Message;
			}
		}

		void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Contact;
			if (item != null)
			{
				Navigation.PushAsync(new ContactSinglePage() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				contactListView.SelectedItem = null;
			
				//IsPresented = false;
			}
		}
	}
}