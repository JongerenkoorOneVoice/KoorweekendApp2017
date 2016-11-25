using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;

namespace KoorweekendApp2017.Pages
{
	public partial class HomePage : ContentPage
	{
        public HomePage()
        {

            InitializeComponent();
            Birthdays();
            Events();

            var linkOpen = new TapGestureRecognizer();
            linkOpen.Tapped += (s, e) => {
                Device.OpenUri(new Uri("http://www.jongerenkooronevoice.nl/"));
            };
            label.GestureRecognizers.Add(linkOpen);          
        }


        void Birthdays()
        {
            Contact firstBirthdayContact = HomePageHelper.GetFirstBirthdayContact(App.Database); // Contact met de eerste verjaardag.
            List<Contact> birthdayInNextSevenDays = HomePageHelper.GetBirthdaysInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0)); // Alle jarigen in een bepalde periode.

            DateTime contactsBirthday = firstBirthdayContact.BirthDate.Value;
            var birtDayThisYear = contactsBirthday.AddYears(DateTime.Now.Year - contactsBirthday.Year);
            var days = (birtDayThisYear.Day - DateTime.Now.Day);
            var age = (DateTime.Now.Year - contactsBirthday.Year);

            birthName.Text = (firstBirthdayContact.FullName);
            if (days > 1)
            {
                birthDate.Text = (string.Format("Wordt over {0} dagen", (Convert.ToString(days))));
                birthAge.Text = (string.Format("{0} jaar", (Convert.ToString(age))));
            }
            else if (days == 1)
            {
                birthDate.Text = (string.Format("Wordt morgen", (Convert.ToString(days))));
                birthAge.Text = (string.Format("{0} jaar", (Convert.ToString(age))));
            }
            else if (days == 0)
            {
                birthDate.Text = ("Is vandaag");
                birthAge.Text = (string.Format("{0} jaar", (Convert.ToString(age))));
                birthToday.Text = ("Geworden");
                birthClick.Text = ("Klik om contactpagina te openen");
                var jarigOpen = new TapGestureRecognizer();
                jarigOpen.Tapped += (s, e) => {
                    Device.OpenUri(new Uri("http://www.jongerenkooronevoice.nl/"));
                };
                birthGrid.GestureRecognizers.Add(jarigOpen);
            }
        }
        void Events()
        {
            Event nextEvent = HomePageHelper.GetNextEvent(App.Database); // Het eerstvolgende event.
            List<Event> eventsInNextSevenDays = HomePageHelper.GetEventsInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0)); // Alle eventementen in een bepalde periode.
            var days = (nextEvent.StartDate.Day - DateTime.Now.Day);

            if (days > 1)
            {
                eventTitle.Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title))));
                eventDatum.Text = (string.Format("{0}", (nextEvent.StartDate.ToString("dd MMMM yyyy"))));
                eventTime.Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm"))));
            }
            else if (days == 1)
            {
                eventTitle.Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title))));
                eventDatum.Text = (string.Format("Morgen"));
                eventTime.Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm"))));
            }
            else if (days == 0)
            {
                eventTitle.Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title))));
                eventDatum.Text = (string.Format("Vandaag"));
                eventTime.Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm"))));
                eventClick.Text = ("Klik om evenementpagina te openen");
                var eventOpen = new TapGestureRecognizer();
                eventOpen.Tapped += (s, e) => {
                    Device.OpenUri(new Uri("http://www.jongerenkooronevoice.nl/"));
                };
                eventGrid.GestureRecognizers.Add(eventOpen);
                //eventGrid.BackgroundColor = Color.FromRgba(255,0,0,255);
            }

        }
    }
}
