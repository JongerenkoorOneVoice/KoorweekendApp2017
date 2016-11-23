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
            var linkOpen = new TapGestureRecognizer();
            linkOpen.Tapped += (s, e) => {
                Device.OpenUri(new Uri("http://www.jongerenkooronevoice.nl/"));
            };
            label.GestureRecognizers.Add(linkOpen);

			/**
			 * Hoi Daniel,
			 * 
			 * Ik heb een paar functies gemaakt die wat data voro de homepage op kunnen halen.
			 * Ik hoop dat je er wat aan hebt.
			 * Als je er niet uit komt, of vragen hebt, dan geef je maar een seintje.
			 * 
			 * De code staat in \Helpers\HomepageHelper.cs
			 * 
			 * Heel veel succes!
			 * 
			*/

			// Contact met de eerste verjaardag.
			// Als er meerdere jarig zijn op dezelfde datum dan wordt diegene wiens voornaam het laatst in het alfabet komt teruggegeven.
			Contact firstBirthdayContact = HomePageHelper.GetFirstBirthdayContact(App.Database);

			// Alle jarigen in een bepalde periode.
			// In dit voorbeeld iedereen die jarig is in de komende 7 dagen.
			List<Contact> birthdayInNextSevenDays = HomePageHelper.GetBirthdaysInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0));

			// Het eerstvolgende event.
			Event nextEvent = HomePageHelper.GetNextEvent(App.Database);

			// Alle eventementen in een bepalde periode.
			// In dit voorbeeld iedereen die jarig is in de komende 7 dagen.
			List<Event> eventsInNextSevenDays = HomePageHelper.GetEventsInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0));


        }

    }
}
