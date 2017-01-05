using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoorweekendApp2017.BusinessObjects;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Interfaces;
using KoorweekendApp2017.Models;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace KoorweekendApp2017
{
	public static class HomePageHelper
	{

		/// <summary>  
		/// Returns one contact with the first birthday.
		/// If multiple contacts have the same birthday only the the contact whoes first name is last in the alphabeth will be returned.
		/// </summary>  
		public static Contact GetFirstBirthdayContact(IOneVoiceDatabase db)
		{
			Contact returnValue = default(Contact);

			List<Contact> allContacts = db.Contacts.GetAll();
			DateTime today = DateTime.Now.Date;
			DateTime firstBirtdayDate = DateTime.MaxValue;

			foreach (Contact contact in allContacts)
			{
				if (contact.BirthDate != null && contact.BirthDate != DateTime.MinValue)
				{
					DateTime contactsBirthday = contact.BirthDate.Value;

					var birtDayThisYear = contactsBirthday.AddYears(DateTime.Now.Year - contactsBirthday.Year);
					var nextBirthday = birtDayThisYear;
					if (birtDayThisYear < DateTime.Now.Date)
					{
						nextBirthday = nextBirthday.AddYears(1);
					}

					if (nextBirthday >= today && nextBirthday <= firstBirtdayDate)
					{
						firstBirtdayDate = nextBirthday;
						returnValue = contact;
					}

				}
			}
			return returnValue == default(Contact) ? null : returnValue;

		}

		/// <summary>  
		/// Returns all contacts with a birthday in the timespan.
		/// Contacts are ordered by birthday first and by name next.
		/// </summary>  
		public static List<Contact> GetBirthdaysInTimeSpan(IOneVoiceDatabase db, TimeSpan timeSpan)
		{
			List<Contact> returnValue = new List<Contact>();

			List<Contact> allContacts = db.Contacts.GetAll();
			DateTime today = DateTime.Now.Date;
			DateTime lastBirtdayDate = today.Add(timeSpan).Date;
			Contact temp;

			foreach (Contact contact in allContacts)
			{
				temp = contact;
				if (contact.BirthDate != null && contact.BirthDate != DateTime.MinValue)
				{
					DateTime contactsBirthday = contact.BirthDate.Value.Date;
					var birtDayThisYear = contactsBirthday.AddYears(DateTime.Now.Year - contactsBirthday.Year).Date;
					var nextBirthday = birtDayThisYear;
					if (birtDayThisYear < DateTime.Now.Date)
					{
						nextBirthday = nextBirthday.AddYears(1);
					}

					if (nextBirthday >= today && nextBirthday <= lastBirtdayDate)
					{
						returnValue.Add(contact);
					}

				}
			}


			return returnValue.OrderBy(x => x.BirthDate).ThenBy(x => x.FirstName).ToList();
		}

		/// <summary>  
		/// Returns the nearest event.
		/// </summary>  
		public static Event GetNextEvent(IOneVoiceDatabase db)
		{
			Event returnValue = default(Event);

			List<Event> allEvents = db.Events.GetAll();
			DateTime today = DateTime.Now.Date;
			DateTime firstEventDateTime = DateTime.MaxValue;

			foreach (Event eventItem in allEvents)
			{

				DateTime eventDateTime = eventItem.StartDate;

				if (eventItem.StartTime != DateTime.MinValue)
				{
					if (eventDateTime.Hour == 0) eventDateTime.AddHours(eventItem.StartTime.Hour);
					if (eventDateTime.Minute == 0) eventDateTime.AddHours(eventItem.StartTime.Minute);
					if (eventDateTime.Second == 0) eventDateTime.AddHours(eventItem.StartTime.Second);
				}


				if (eventDateTime >= DateTime.Today && eventDateTime <= firstEventDateTime)
				{
					firstEventDateTime = eventDateTime;
					returnValue = eventItem;
				}

			}
			return returnValue == default(Event) ? null : returnValue;
		}

		/// <summary>  
		/// Returns all event in timespan ordered by startdate and -time.
		/// </summary>  
		public static List<Event> GetEventsInTimeSpan(IOneVoiceDatabase db, TimeSpan timeSpan)
		{
			List<Event> returnValue = new List<Event>();

			List<Event> allEvents = db.Events.GetAll();
			DateTime today = DateTime.Now.Date;
			DateTime maxDateTime = today.Add(timeSpan);

			foreach (Event eventItem in allEvents)
			{

				DateTime eventDateTime = eventItem.StartDate;

				if (eventItem.StartTime != DateTime.MinValue)
				{
					if (eventDateTime.Hour == 0) eventDateTime.AddHours(eventItem.StartTime.Hour);
					if (eventDateTime.Minute == 0) eventDateTime.AddHours(eventItem.StartTime.Minute);
					if (eventDateTime.Second == 0) eventDateTime.AddHours(eventItem.StartTime.Second);
				}


				if (eventDateTime >= DateTime.Now && eventDateTime <= maxDateTime)
				{
					returnValue.Add(eventItem);
				}

			}
			return returnValue.OrderBy(x => x.StartDate).ThenBy(x => x.StartTime).ToList();
		}

		/// <summary>  
		/// Returns the last changed newsItem.
		/// </summary>  
		public static List<News> GetLastChangedNewsItem(IOneVoiceDatabase db)
		{
			return db.News.GetAll().FindAll(x => x.IsVisible != false).OrderBy(x => x.LastModified).ToList();

		}

		/// <summary>  
		/// Returns Bibleverse for today from https://www.dagelijkswoord.nl/
		//// </summary>  
		public async static Task<DailyBread> GetBibleVerseForToday()
		{

			var dailyBread = default(DailyBread);

			DateTime lastSync = App.Database.Settings.GetValue<DateTime>("lastDailyBreadSync");

			if (lastSync.Date < DateTime.Now.Date)
			{

				var network = Resolver.Resolve<INetwork>();
				if (network.InternetConnectionStatus() != NetworkStatus.NotReachable)
				{
					var auth = new BasicAuthentication();
					auth.UserName = "apponevoice@gmail.com";
					auth.PassWord = "b33c948926b31c23715536e8adac94b8fcd940a3";

					dailyBread = await RestHelper.GetRestDataFromUrl<DailyBread>("http://feed.dagelijkswoord.nl/api/json/1.0", auth);
					App.Database.Settings.Set("lastDailyBreadSync", DateTime.Now);
					App.Database.Settings.Set("lastDailyBreadSyncData", dailyBread);
				}
			}
			else
			{
				dailyBread = App.Database.Settings.GetValue<DailyBread>("lastDailyBreadSyncData");
			
			}
			return dailyBread;
		}


	}
}
