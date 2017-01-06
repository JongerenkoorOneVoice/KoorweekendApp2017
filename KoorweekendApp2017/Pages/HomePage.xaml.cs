using System;
using System.Collections.Generic;
using KoorweekendApp2017.Models;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using System.Threading.Tasks;

namespace KoorweekendApp2017.Pages
{

	public partial class HomePage : ContentPage
	{

		public bool VerseLoaded { get; set;}

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
		public List<News> allNews = HomePageHelper.GetLastChangedNewsItem(App.Database);

		public DateTime datumBirth;
		public DateTime datumEvent;
        public Int32 index = -1;
        public Int32 counter = 1;
        public DailyBread Verse;

        public HomePage()
		{
			InitializeComponent();
			this.BindingContext = this;

			HomePageHelper.GetBibleVerseForToday().ContinueWith((request) =>
			{

				/* Hoi Daniel,
				 * -----------
				 * Hieronder vind je de Bijbeltekst van de dag in de variabele dayTexts.
				 * Hij haalt de teksten van de afgelopen drie dagen op.
				 * Het duurt even voor hij de data heeft ogpgehaald. Daarom gebruik ik de ContinueWidth functie.
				 * Het is de bedoeling dat je je code binnen de accolades van deze functie schrijft.
				 * De homepage laadt dan gewoon door en start alvast. Zodra de data geladen is komt hij in deze functie.
				 * Dit kan heel snel gaan, want als het vers van vandaag al is opgehaald, dan doet hij dat niet opnieuw.
				 * Als je dit te lastig vindt, laat het dan even weten. Het kan ook nog op een andere manier (maar dat is meer werk).
				 * Als je vragen hebt, dan hoor ik het wel.
				 */

				DailyBread dayTexts = request.Result;
                Verse = dayTexts;
				VerseLoaded = true;


                // Jouw code hier.
                // (Je kan bijvoorbeeld een publieke property maken met dit object en die in News() inladen?)

            });

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
            stack1.BackgroundColor = Color.Red;


            var newsOpen = new TapGestureRecognizer();
            newsOpen.Tapped += (s, e) =>
            {
                if (counter == 2)
                {
                    Navigation.PushAsync(new DailyBibleVersSingle() { BindingContext = Verse.data[0]});
                }
                if (counter >= 3)
                {
                    if (index == -1)
                    {
                        Navigation.PushAsync(new NewsArchive());
                    }
                    else
                    {
                        Navigation.PushAsync(new NewsSingle() { BindingContext = allNews[index] });
                    }
                }
            };
            stack1.GestureRecognizers.Add(newsOpen);
            stackNews.Children.Add(new Label { Text = "Vers van de dag", FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });

            stack1.Children.Add(stackNews);
            controlGrid.Children.Add(stack1, 1, 3, 0, 1);
            this.Content = controlGrid;

			Task.Run(() => { 
				while (!VerseLoaded){}
				Task.Delay(2000).Wait();
				StartNewsAnimation(stack1, stackNews);
				Device.StartTimer(TimeSpan.FromSeconds(8), () =>
				{
					StartNewsAnimation(stack1, stackNews);
				
					return true;
				});
			});

        }

		void StartNewsAnimation(StackLayout stack1, StackLayout stackNews)
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
					//await stackNews.RotateXTo(90, 2000);
				   Task labelFadeO = stackNews.FadeTo(0, 1000);
				   Task gridFadeO = stack1.FadeTo(0.3, 1000);
				   await Task.WhenAll(new List<Task> { labelFadeO, gridFadeO });
				   stackNews.Children.Clear();
				   if (counter == 0)
				   {
					   stackNews.Children.Add(new Label { Text = "Vers van de dag", FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
					   counter++;
				   }
				   else if (counter == 1)
				   {

					   var versje = (Convert.ToString(Verse.data[0].text.hsv));
					   if (versje.Length > 120)
					   {
						   var temp = versje.Substring(0, 120).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
						   versje = String.Join(" ", temp, 0, temp.Length - 1).Trim();
						   versje += "...";
					   }
					   stackNews.Children.Add(new Label { Text = (Convert.ToString(versje)), FontSize = 12, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
					   stackNews.Children.Add(new Label { Text = (Convert.ToString(Verse.data[0].source)), FontSize = 10, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
					   if (allNews != null)
					   {
						   counter++;
					   }
					   else
					   {
						   counter = 0;
					   }
				   }
				   else if (counter == 2)
				   {
					   stackNews.Children.Add(new Label { Text = "Nieuws", FontAttributes = FontAttributes.Bold, FontSize = 22, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
					   counter++;
                       index = -1;
                }
				   else if (counter >= 3)
				   {
					   if (index < allNews.Count - 1)
					   {
						   index++;
					   }
					   else if (index >= allNews.Count - 1)
					   {
						   index = 0;
					   }

					   stackNews.Children.Add(new Label { Text = (string.Format("{0}", (Convert.ToString(allNews[index].Title)))), FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
					   stackNews.Children.Add(new Label { Text = (string.Format("{0}", (Convert.ToString(allNews[index].LastModifiedDateFormatted)))), HorizontalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, TextColor = Color.White });
					   if (index >= allNews.Count - 1)
					   {
						   counter = 0;
					   }
				   }
				   Task labelFadeI = stackNews.FadeTo(1, 1000);
				   Task gridFadeI = stack1.FadeTo(1, 1000);
				   await Task.WhenAll(new List<Task> { labelFadeI, gridFadeI });

			   });
	
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
 