using System;
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
                Int32 screen = Convert.ToInt32(device.Display.Width / (3 * (device.Display.Scale)));
                return screen;
            }
        }
        public Int32 ScreenHeight
        {
            get
            {
                var device = Resolver.Resolve<IDevice>();
                Int32 screenW = Convert.ToInt32(device.Display.Width / (3 * (device.Display.Scale)));
                Int32 screenH = Convert.ToInt32(device.Display.Height / (device.Display.Scale));
                Int32 screen = Convert.ToInt32((screenH - screenW) / 2);
                return screen;
            }
        }
        public Contact firstBirthdayContact = HomePageHelper.GetFirstBirthdayContact(App.Database);
        public Event nextEvent = HomePageHelper.GetNextEvent(App.Database);
        public DateTime datumBirth;
        public DateTime datumEvent;

        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = this;

            if (firstBirthdayContact != null)
            {
                datumBirth = firstBirthdayContact.BirthDate.Value.AddYears(DateTime.Now.Year - firstBirthdayContact.BirthDate.Value.Year);
                if (datumBirth < DateTime.Now.Date)
                {
                    datumBirth = datumBirth.AddYears(1);
                }
            }
            if (nextEvent != null)
            {
                datumEvent = nextEvent.StartDate;
            }

            Birthdays();
            Events();
            News();

            var linkOpen = new TapGestureRecognizer();
            linkOpen.Tapped += (s, e) =>
            {
                Device.OpenUri(new Uri("http://www.jongerenkooronevoice.nl/"));
            };
            label.GestureRecognizers.Add(linkOpen);

        }

        void News()
        {
            var stack1 = new StackLayout();
            var stackNews = new StackLayout();
            stackNews.VerticalOptions = LayoutOptions.CenterAndExpand;
            stackNews.Padding = new Thickness(20, 0, 20, 0);

            bool news = true;
            if (news == false)
            {
                stack1.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
            }
            else if (news == true)
            {
                stack1.BackgroundColor = Color.Red;
                stackNews.Children.Add(new Label { Text = "Nieuwsupdate:", FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                stackNews.Children.Add(new Label { Text = "U heeft 7 ongelezen updates", HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                var newsOpen = new TapGestureRecognizer();
                newsOpen.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new NewsArchive());
                };
                stack1.GestureRecognizers.Add(newsOpen);
            }
            stack1.Children.Add(stackNews);
            controlGrid.Children.Add(stack1, 1, 3, 0, 1);
            this.Content = controlGrid;
        }


        void Birthdays()
        {
            //List<Contact> birthdayInNextSevenDays = HomePageHelper.GetBirthdaysInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0)); // Alle jarigen in een bepalde periode.
            if (firstBirthdayContact != null)
            {
                var stack1 = new StackLayout();
                var stackBirth = new StackLayout();
                stackBirth.VerticalOptions = LayoutOptions.CenterAndExpand;
                stackBirth.Padding = new Thickness(20, 0, 20, 0);

                var timeDifference = (datumBirth - DateTime.Today);

                if (timeDifference.Days <= 30)
                {
                    stack1.BackgroundColor = Color.Red;

                    var age = (datumBirth.Year - firstBirthdayContact.BirthDate.Value.Year);

                    stackBirth.Children.Add(new Label { Text = (firstBirthdayContact.FullName), FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                    if (timeDifference.Days > 1)
                    {
                        stackBirth.Children.Add(new Label { Text = (string.Format("Wordt over {0} dagen", (Convert.ToString(timeDifference.Days)))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackBirth.Children.Add(new Label { Text = (string.Format("{0} jaar", (Convert.ToString(age)))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                    }
                    else if (timeDifference.Days == 1)
                    {
                        stackBirth.Children.Add(new Label { Text = (string.Format("Wordt morgen", (Convert.ToString(timeDifference.Days)))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackBirth.Children.Add(new Label { Text = (string.Format("{0} jaar", (Convert.ToString(age)))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                    }
                    else if (timeDifference.Days == 0)
                    {
                        stackBirth.Children.Add(new Label { Text = ("Is vandaag"), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackBirth.Children.Add(new Label { Text = (string.Format("{0} jaar", (Convert.ToString(age)))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackBirth.Children.Add(new Label { Text = ("Geworden"), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
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
                    stack1.GestureRecognizers.Add(jarigOpen);

                    if (datumBirth < datumEvent | nextEvent == null)
                    {
                        stack1.Children.Add(stackBirth);
                        controlGrid.Children.Add(stack1, 0, 2, 1, 2);
                    }
                    else
                    {
                        stack1.Children.Add(stackBirth);
                        controlGrid.Children.Add(stack1, 1, 3, 2, 3);
                    }

                    this.Content = controlGrid;
                }
                else
                {
                    stack1.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                }
            }
        }


        void Events()
        {
            //List<Event> eventsInNextSevenDays = HomePageHelper.GetEventsInTimeSpan(App.Database, new TimeSpan(7, 0, 0, 0)); // Alle eventementen in een bepalde periode.
            if (nextEvent != null)
            {
                var stack2 = new StackLayout();
                var stackEvent = new StackLayout();
                stackEvent.VerticalOptions = LayoutOptions.CenterAndExpand;
                stackEvent.Padding = new Thickness(20, 0, 20, 0);

                var timeDifference = (nextEvent.StartDate - DateTime.Today);

                if (timeDifference.Days <= 30)
                {
                    stack2.BackgroundColor = Color.Red;

                    if (timeDifference.Days > 1)
                    {
                        stackEvent.Children.Add(new Label { Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title)))), FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackEvent.Children.Add(new Label { Text = (string.Format("{0}", (nextEvent.StartDate.ToString("dd MMMM yyyy")))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        if ((nextEvent.StartTime.ToString("HH:mm")) != "00:00")
                        {
                            stackEvent.Children.Add(new Label { Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm")))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        }
                    }
                    else if (timeDifference.Days == 1)
                    {
                        stackEvent.Children.Add(new Label { Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title)))), FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackEvent.Children.Add(new Label { Text = (string.Format("Morgen")), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        if ((nextEvent.StartTime.ToString("HH:mm")) != "00:00")
                        {
                            stackEvent.Children.Add(new Label { Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm")))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        }
                    }
                    else if (timeDifference.Days == 0)
                    {
                        stackEvent.Children.Add(new Label { Text = (string.Format("{0}", (Convert.ToString(nextEvent.Title)))), FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        stackEvent.Children.Add(new Label { Text = (string.Format("Vandaag")), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        if ((nextEvent.StartTime.ToString("HH:mm")) != "00:00")
                        {
                            stackEvent.Children.Add(new Label { Text = (string.Format("{0} uur", (nextEvent.StartTime.ToString("HH:mm")))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
                        }
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
                    stack2.GestureRecognizers.Add(eventOpen);

                    if (datumEvent < datumBirth | firstBirthdayContact == null)
                    {
                        stack2.Children.Add(stackEvent);
                        controlGrid.Children.Add(stack2, 0, 2, 1, 2);
                    }
                    else
                    {
                        stack2.Children.Add(stackEvent);
                        controlGrid.Children.Add(stack2, 1, 3, 2, 3);
                    }
                    this.Content = controlGrid;
                }
                else
                {
                    stack2.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                }
            }
        }
    }
}