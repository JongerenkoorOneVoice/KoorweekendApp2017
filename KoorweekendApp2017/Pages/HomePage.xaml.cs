﻿using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;

namespace KoorweekendApp2017.Pages
{
    public partial class HomePage : ContentPage
    {
        public Int32 ScreenWidth
        {
            get
            {
                var device = Resolver.Resolve<IDevice>();
                Int32 screen = Convert.ToInt32(device.Display.Width / (3*(device.Display.Scale)));
                return screen;
            }
        }
        public Int32 ScreenHeight
        {
            get
            {
                var device = Resolver.Resolve<IDevice>();
                Int32 screenW = Convert.ToInt32(device.Display.Width / (3*(device.Display.Scale))); 
                Int32 screenH = Convert.ToInt32(device.Display.Height / (device.Display.Scale));
                Int32 screen = Convert.ToInt32((screenH - screenW)/2);
                return screen;
            }
        }

        public HomePage()
        {
            InitializeComponent();
            Birthdays();
            Events();
            News();
           

            this.BindingContext = this;

            var linkOpen = new TapGestureRecognizer();
            linkOpen.Tapped += (s, e) =>
            {
                Device.OpenUri(new Uri("http://www.jongerenkooronevoice.nl/"));
            };
            label.GestureRecognizers.Add(linkOpen);

        }

        void News()
        {
            bool news = true;
                if (news == false) {
                newsGrid.BackgroundColor = Color.FromRgba(255, 0, 0, 0);
            }
            else if(news == true)
            {
                newsTitle.Text = "Nieuwsupdate:";
                newsText.Text = "U heeft 7 ongelezen updates";
                //newsClick.Text = "Klik om de nieuwspagina te openen";
                newsGrid.BackgroundColor = Color.FromRgba(255, 0, 0, 255);
                var newsOpen = new TapGestureRecognizer();
                newsOpen.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new NewsArchive());
                };
                newsGrid.GestureRecognizers.Add(newsOpen);
            }
        }


        void Birthdays()
        {
            Contact firstBirthdayContact = HomePageHelper.GetFirstBirthdayContact(App.Database); // Contact met de eerste verjaardag.
            List<Contact> birthdayInNextSevenDays = HomePageHelper.GetBirthdaysInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0)); // Alle jarigen in een bepalde periode.
			if (firstBirthdayContact != null)
			{

				DateTime contactsBirthday = firstBirthdayContact.BirthDate.Value;
				var birtDayThisYear = contactsBirthday.AddYears(DateTime.Now.Year - contactsBirthday.Year);
                if (birtDayThisYear < DateTime.Now.Date)
                {
                    birtDayThisYear = birtDayThisYear.AddYears(1);
                }
                var timeDifference = (birtDayThisYear - DateTime.Today);

                if (timeDifference.Days <= 30)
                {
                    var age = (birtDayThisYear.Year - contactsBirthday.Year);

                    birthName.Text = (firstBirthdayContact.FullName);
                    if (timeDifference.Days > 1)
                    {
                        birthDate.Text = (string.Format("Wordt over {0} dagen", (Convert.ToString(timeDifference.Days))));
                        birthAge.Text = (string.Format("{0} jaar", (Convert.ToString(age))));
                    }
                    else if (timeDifference.Days == 1)
                    {
                        birthDate.Text = (string.Format("Wordt morgen", (Convert.ToString(timeDifference.Days))));
                        birthAge.Text = (string.Format("{0} jaar", (Convert.ToString(age))));
                    }
                    else if (timeDifference.Days == 0)
                    {
                        birthDate.Text = ("Is vandaag");
                        birthAge.Text = (string.Format("{0} jaar", (Convert.ToString(age))));
                        birthToday.Text = ("Geworden");
                        //birthClick.Text = ("Klik om contactpagina te openen");
                    }
                    var jarigOpen = new TapGestureRecognizer();
                    jarigOpen.Tapped += (s, e) =>
                    {
                        var item = firstBirthdayContact;
                        if (item != null)
                        {
                            Navigation.PushAsync(new ContactSinglePage() { BindingContext = item });
                        }
                    };
                    birthGrid.GestureRecognizers.Add(jarigOpen);
                }
                else
                {
                    birthGrid.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                }
			}

        }


        void Events()
        {
            Event nextEvent = HomePageHelper.GetNextEvent(App.Database); // Het eerstvolgende event.
            //List<Event> eventsInNextSevenDays = HomePageHelper.GetEventsInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0)); // Alle eventementen in een bepalde periode.
            if(nextEvent != null)
            {
                var timeDifference = (nextEvent.StartDate - DateTime.Today);
                eventTitle.Text = (string.Format("{0}", (Convert.ToString(timeDifference))));

                if (timeDifference.Days > 1)
                {
                    eventTitle.Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title))));
                    eventDatum.Text = (string.Format("{0}", (nextEvent.StartDate.ToString("dd MMMM yyyy"))));
                    if ((nextEvent.StartTime.ToString("HH:mm")) != "00:00")
                    {
                        eventTime.Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm"))));
                    }
                }
                else if (timeDifference.Days == 1)
                {
                    eventTitle.Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title))));
                    eventDatum.Text = (string.Format("Morgen"));
                    eventTime.Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm"))));
                }
                else if (timeDifference.Days == 0)
                {
                    eventTitle.Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title))));
                    eventDatum.Text = (string.Format("Vandaag"));
                    eventTime.Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm"))));
                    //eventClick.Text = ("Klik om evenementpagina te openen");
                    //eventGrid.BackgroundColor = Color.FromRgba(255,0,0,255);
                }
                var eventOpen = new TapGestureRecognizer();
                eventOpen.Tapped += (s, e) =>
                {
                    var item = nextEvent;
                    if (item != null)
                    {
                        Navigation.PushAsync(new EventSinglePage() { BindingContext = item });
                    }
                };
                eventGrid.GestureRecognizers.Add(eventOpen);
            }
            

        }
    }
}