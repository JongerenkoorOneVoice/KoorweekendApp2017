using System;
using System.Collections.Generic;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;

namespace KoorweekendApp2017.Tasks
{
	public static class DataSync
	{
		public static void UpdateContactsInDbFromApi()
		{

			DateTime lastUpdate = DateTime.Parse("1010-01-01");
			Setting lastUpdatedSetting = App.Database.Settings.GetByKey("lastContactsUpdate");
			if (lastUpdatedSetting != null)
			{
				lastUpdate = DateTime.Parse(lastUpdatedSetting.Value);
				lastUpdate = lastUpdate.AddDays(-1);
			}

			string query = String.Format("http://www.jongerenkooronevoice.nl/contacts/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd") );
			List<Contact> contacts = RestHelper.GetRestDataFromUrl<Contact>(query).Result;
			if (contacts != null)
			{
				foreach (Contact contact in contacts)
				{
					App.Database.Contacts.UpdateOrInsert(contact);
				}
			}
			App.Database.Settings.Set("lastContactsUpdate", DateTime.Now.ToString());
		}

		public static void UpdateSongsInDbFromApi()
		{
			DateTime lastUpdate = DateTime.Parse("1010-01-01");
			Setting lastUpdatedSetting = App.Database.Settings.GetByKey("lastSongUpdate");
			if (lastUpdatedSetting != null)
			{
				lastUpdate = DateTime.Parse(lastUpdatedSetting.Value);
				lastUpdate = lastUpdate.AddDays(-1);
			}

			string query = String.Format("http://www.jongerenkooronevoice.nl/songs/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
			List<Song> songs = RestHelper.GetRestDataFromUrl<Song>(query).Result;
			if (songs != null)
			{
				foreach (Song song in songs)
				{
					App.Database.Songs.UpdateOrInsert(song);

				}
			}
			App.Database.Settings.Set("lastSongUpdate", DateTime.Now.ToString());
		}

		public static void UpdateEventsInDbFromApi()
		{
			DateTime lastUpdate = DateTime.Parse("1010-01-01");
			Setting lastUpdatedSetting = App.Database.Settings.GetByKey("lastEventsUpdate");
			if (lastUpdatedSetting != null)
			{
				lastUpdate = DateTime.Parse(lastUpdatedSetting.Value);
				lastUpdate = lastUpdate.AddDays(-1);
			}

			string query = String.Format("http://www.jongerenkooronevoice.nl/apievents/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
			List<Event> events = RestHelper.GetRestDataFromUrl<Event>(query).Result;
			if (events != null)
			{
				foreach (Event eventItem in events)
				{
					App.Database.Events.UpdateOrInsert(eventItem);

				}
			}
			App.Database.Settings.Set("lastEventsUpdate", DateTime.Now.ToString());
		}

	}
}
