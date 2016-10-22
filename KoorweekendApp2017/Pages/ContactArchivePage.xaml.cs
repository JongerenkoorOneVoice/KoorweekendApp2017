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
        public List<Contact> Contacts = new List<Contact>();
        public String oldSearchValue = String.Empty;
        public ContactArchivePage()
		{
			try
			{
				InitializeComponent();
				contactListView.ItemSelected += OnContactSelected;
				Contacts = App.Database.Contacts.GetAll();
				Contacts = Contacts.OrderBy(Contact => Contact.FirstName).ToList();
				contactListView.ItemsSource = Contacts;
                mainSearchBar.TextChanged += OnSearchButtonPressed;
                mainSearchBar.Focused += MainSearchFocused;



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
        void OnSearchButtonPressed(object sender, EventArgs args)
        {
            string searchValue = mainSearchBar.Text.ToLower();
            if (searchValue == String.Empty)
            {
                contactListView.ItemsSource = Contacts;
            }
            else
            {
                List<Contact> foundContacts = Contacts.FindAll(
                    x => x.FullName.ToLower().Contains(searchValue) == true);
                contactListView.ItemsSource = foundContacts;
            }

            oldSearchValue = searchValue;
        }

        void MainSearchFocused(object sender, EventArgs args)
        {

            string searchValue = mainSearchBar.Text == null ? String.Empty : mainSearchBar.Text.ToLower();
            if (String.IsNullOrEmpty(searchValue) && oldSearchValue.Length > 0)
            {
                //mainSearchBar.Unfocus();
                contactListView.Focus();
            }
     
        }
        

    }
}