using System;
using System.Collections.Generic;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Xamarin.Forms;
using System.Linq;
using KoorweekendApp2017.Tasks;

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
				ContactListView.ItemSelected += OnContactSelected;
				ContactListView.IsPullToRefreshEnabled = true;
				ContactListView.Refreshing += ReloadContactsFromWebservice;
				SetupContactDataForList();



                mainSearchBar.TextChanged += OnTextChanged;
                mainSearchBar.Focused += MainSearchFocused;



            }
			catch (Exception ex)
			{
				var a = ex.Message;
			}
		}

		private void SetupContactDataForList()
		{
			Contacts = App.Database.Contacts.GetAll();
			Contacts = Contacts.OrderBy(Contact => Contact.FirstName).ToList();
			ContactListView.ItemsSource = Contacts;
		}

		void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Contact;
			if (item != null)
			{
				Navigation.PushAsync(new ContactSinglePage() { BindingContext = item });
				//Detail = new MainNavigationPage((Page)Activator.CreateInstance(typeof(ContactSinglePage)));
				ContactListView.SelectedItem = null;
			
				//IsPresented = false;
			}
		}
        void OnTextChanged(object sender, EventArgs args)
        {
            string searchValue = mainSearchBar.Text == null ? String.Empty : mainSearchBar.Text.ToLower();
            if (searchValue == String.Empty)
            {
                ContactListView.ItemsSource = Contacts;
            }
            else
            {
                List<Contact> foundContacts = Contacts.FindAll(
                    x => x.FullName.ToLower().Contains(searchValue) == true);
                ContactListView.ItemsSource = foundContacts;
            }

            oldSearchValue = searchValue;
        }

        void MainSearchFocused(object sender, EventArgs args)
        {

            string searchValue = mainSearchBar.Text == null ? String.Empty : mainSearchBar.Text.ToLower();
            if (String.IsNullOrEmpty(searchValue) && oldSearchValue.Length > 0)
            {
                //mainSearchBar.Unfocus();
                ContactListView.Focus();
            }
     
        }
        
		void ReloadContactsFromWebservice(object sender, EventArgs args)
		{
			DataSync.UpdateContactsInDbFromApi(true);
			SetupContactDataForList();
			ListView listView = sender as ListView;
			listView.EndRefresh();

		}
    }
}