using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class ContactSinglePage : ContentPage
	{
		public ContactSinglePage()
		{

            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Convert.ToString(Email.Text).ToLower() != "geen")
            {
                Email.TextColor = Color.FromHex("#0645AD");
                var emailOpen = new TapGestureRecognizer();
                emailOpen.Tapped += (s, e) => {
                        Device.OpenUri(new Uri(string.Format("mailto:{0}", Email.Text)));
                };
                Email.GestureRecognizers.Add(emailOpen);
            }

            if (Convert.ToString(Mobiel.Text).ToLower() != "geen")
            {
                Mobiel.TextColor = Color.FromHex("#0645AD");
                var mobileOpen = new TapGestureRecognizer();
                mobileOpen.Tapped += (s, e) =>
                {
                        Device.OpenUri(new Uri(string.Format("tel:{0}", Mobiel.Text)));
                };
                Mobiel.GestureRecognizers.Add(mobileOpen);
            }

            if (Convert.ToString(Telefoon.Text).ToLower() != "geen")
            {
                Telefoon.TextColor = Color.FromHex("#0645AD");
                var phoneOpen = new TapGestureRecognizer();
                phoneOpen.Tapped += (s, e) =>
                {
                        Device.OpenUri(new Uri(string.Format("tel:{0}", Telefoon.Text)));
                };
                Telefoon.GestureRecognizers.Add(phoneOpen);
            }
        }
    }
}
