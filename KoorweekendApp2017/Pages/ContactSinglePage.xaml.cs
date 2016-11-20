
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
            /*
            Contact contact = new Contact();

            DateTime birthDate = contact.BirthDate == null ? DateTime.MinValue : (DateTime)contact.BirthDate;
            if(birthDate != DateTime.MinValue)
            {
                DateTime nextBirthday = birthDate;
                nextBirthday = nextBirthday.AddYears(DateTime.Now.Year - birthDate.Year);
                if(nextBirthday < DateTime.Now)
                {
                    nextBirthday = nextBirthday.AddYears(1);
                }
                
            }
            */

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Convert.ToString(Adres.Text).ToLower() != "geen" && Convert.ToString(Adres.Text).ToLower() != "x" && Convert.ToString(Adres.Text).ToLower() != "onbekend")
            {
                Adres.TextColor = Color.FromHex("#FF0000");//#0645AD voor blauw
                var adresOpen = new TapGestureRecognizer();
                adresOpen.Tapped += (s, e) => {
                    Device.OpenUri(new Uri(string.Format("https://www.google.nl/maps/place/{0},{1}", Adres.Text.Replace(' ', '+'), Plaats.Text)));
                };
                Adres.GestureRecognizers.Add(adresOpen);
            }

            if (Convert.ToString(Email.Text).ToLower() != "geen" && Convert.ToString(Email.Text).ToLower() != "x" && Convert.ToString(Email.Text).ToLower() != "onbekend")
            {
                Email.TextColor = Color.FromHex("#FF0000");
                var emailOpen = new TapGestureRecognizer();
                emailOpen.Tapped += (s, e) => {
                    Device.OpenUri(new Uri(string.Format("mailto:{0}", Email.Text)));
                };
                Email.GestureRecognizers.Add(emailOpen);
            }

            if (Convert.ToString(Mobiel.Text).ToLower() != "geen" && Convert.ToString(Mobiel.Text).ToLower() != "x" && Convert.ToString(Mobiel.Text).ToLower() != "onbekend")
            {
                Mobiel.TextColor = Color.FromHex("#FF0000");
                var mobileOpen = new TapGestureRecognizer();
                mobileOpen.Tapped += (s, e) =>
                {
                    Device.OpenUri(new Uri(string.Format("tel:{0}", Mobiel.Text)));
                };
                Mobiel.GestureRecognizers.Add(mobileOpen);
            }

            if (Convert.ToString(Telefoon.Text).ToLower() != "geen" && Convert.ToString(Telefoon.Text).ToLower() != "x" && Convert.ToString(Telefoon.Text).ToLower() != "onbekend")
            {
                Telefoon.TextColor = Color.FromHex("#FF0000");
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