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


            var emailOpen = new TapGestureRecognizer();
            emailOpen.Tapped += (s, e) => {
                if (Email.Text.Length >= 5)
                {
                    Device.OpenUri(new Uri(string.Format("mailto:{0}", Email.Text)));
                }
            };
            Email.GestureRecognizers.Add(emailOpen);


            var mobileOpen = new TapGestureRecognizer();
            mobileOpen.Tapped += (s, e) => {
                if (Mobiel.Text.Length >= 5)
                {
                    Device.OpenUri(new Uri(string.Format("tel:{0}", Mobiel.Text)));
                }
            };
            Mobiel.GestureRecognizers.Add(mobileOpen);


            var phoneOpen = new TapGestureRecognizer();
            phoneOpen.Tapped += (s, e) =>
            {
                if(Telefoon.Text.Length >= 5){
                    Device.OpenUri(new Uri(string.Format("tel:{0}", Telefoon.Text)));
                };
            };
            Telefoon.GestureRecognizers.Add(phoneOpen);
        }
    }
}
