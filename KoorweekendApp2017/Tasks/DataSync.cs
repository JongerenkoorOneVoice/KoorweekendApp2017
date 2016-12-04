using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Messages;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace KoorweekendApp2017.Tasks
{
	public static class DataSync
	{

		public static void UpdateContactsInDbFromApi(bool shouldUpdateAll = false)
		{

			//if(CrossConnectivity.Current.IsConnected){

			//	var task = Task.Run(async () =>
			//	{
			//		return await CrossConnectivity.Current.IsReachable("jongerenkooronevoice.nl").ConfigureAwait(false);
			//	});

			//	bool hasInternet =  task.Result;

			//	if (hasInternet)
			//	{
			DateTime lastUpdate = DateTime.Parse("1010-01-01");
			String lastUpdatedString = App.Database.Settings.GetValue<String>("lastContactsUpdate");
			if (!String.IsNullOrEmpty(lastUpdatedString)  && !shouldUpdateAll)
			{
				lastUpdate = DateTime.Parse(lastUpdatedString);
				lastUpdate = lastUpdate.AddDays(-1);
			}

			string query = String.Format("http://www.jongerenkooronevoice.nl/contacts/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));

			List<Contact> contacts = RestHelper.GetRestDataFromUrl<List<Contact>>(query).Result;
			if (contacts != null)
			{
				foreach (Contact contact in contacts)
				{
					App.Database.Contacts.UpdateOrInsert(contact);
				}
			}
			App.Database.Settings.Set("lastContactsUpdate", DateTime.Now.ToString());
			//	}
			//}
		}

		public static void UpdateSongsInDbFromApi(bool shouldUpdateAll = false)
		{
			//if (CrossConnectivity.Current.IsConnected)
			//{

			//	var task = Task.Run(async () =>
			//	{
			//		return await CrossConnectivity.Current.IsReachable("jongerenkooronevoice.nl").ConfigureAwait(false);
			//	});
			//	bool hasInternet = task.Result;

			//	if (hasInternet)
			//	{

					DateTime lastUpdate = DateTime.Parse("1010-01-01");
					String lastUpdatedString = App.Database.Settings.GetValue<String>("lastSongUpdate");
					if (!String.IsNullOrEmpty(lastUpdatedString)  && !shouldUpdateAll)
					{
						lastUpdate = DateTime.Parse(lastUpdatedString);
						lastUpdate = lastUpdate.AddDays(-1);
					}

					string query = String.Format("http://www.jongerenkooronevoice.nl/songs/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
					List<Song> songs = RestHelper.GetRestDataFromUrl<List<Song>>(query).Result;
					if (songs != null)
					{
						foreach (Song song in songs)
						{
							App.Database.Songs.UpdateOrInsert(song);

						}
					}
					App.Database.Settings.Set("lastSongUpdate", DateTime.Now.ToString());
			//	}
			
			//}
		}

		public static void UpdateEventsInDbFromApi(bool shouldUpdateAll = false)
		{

			DateTime lastUpdate = DateTime.Parse("1010-01-01");
			String lastUpdatedString = App.Database.Settings.GetValue<String>("lastEventsUpdate");
			if (!String.IsNullOrEmpty(lastUpdatedString) && !shouldUpdateAll)
			{
				lastUpdate = DateTime.Parse(lastUpdatedString);
				lastUpdate = lastUpdate.AddDays(-1);
			}

			string query = String.Format("http://www.jongerenkooronevoice.nl/apievents/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
			List<Event> events = RestHelper.GetRestDataFromUrl<List<Event>>(query).Result;
			if (events != null)
			{
				foreach (Event eventItem in events)
				{
					App.Database.Events.UpdateOrInsert(eventItem);

				}
			}
			App.Database.Settings.Set("lastEventsUpdate", DateTime.Now.ToString());
		}

		public static void UpdateSongOccasionsInDbFromApi(bool shouldUpdateAll = false)
		{
			//string query = String.Format("http://www.jongerenkooronevoice.nl/apievents/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
			string query = "http://www.jongerenkooronevoice.nl/songoccasions/all";
			List<SongOccasion> occasions = RestHelper.GetRestDataFromUrl<List<SongOccasion>>(query).Result;
			if (occasions != null)
			{
				foreach (SongOccasion occasion in occasions)
				{
					App.Database.SongOccasions.UpdateOrInsert(occasion);

				}
			}
			App.Database.Settings.Set("lastSongOccasionsUpdate", DateTime.Now.ToString());
		}

		public static void SyncPrayerRequests(bool shouldUpdateAll = false)
		{
			//string query = String.Format("http://www.jongerenkooronevoice.nl/apievents/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
			// Get all prayerrequests from the web api

			Int32 currentContactId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");
			List<PrayerRequest> appPrayerRequests = App.Database.PrayerRequests.GetAll();
			List<PrayerRequest> currentContactPrayerRequests = appPrayerRequests.FindAll(x => x.ContactId == currentContactId);
			foreach (var pr in currentContactPrayerRequests){
				if (pr.Id == 0)
				{
					String url = "http://www.jongerenkooronevoice.nl/prayerrequests/createnew";
					RestHelper.PostDataToUrl<PrayerRequest>(url, pr).ContinueWith((arg) =>
					{
						PrayerRequest result = arg.Result;
						result.AppSpecificId = pr.AppSpecificId;
						App.Database.PrayerRequests.UpdateOrInsert(result);
					}); 
				}
				else {
					String url = "http://www.jongerenkooronevoice.nl/prayerrequests/updatebyid/" + Convert.ToString(pr.Id);
					RestHelper.PutDataToUrl<PrayerRequest>(url, pr).ContinueWith((arg) =>
					{
						PrayerRequest result = arg.Result;
						result.AppSpecificId = pr.AppSpecificId;
						App.Database.PrayerRequests.UpdateOrInsert(arg.Result);
					});
				}
					
			}

			string query = "http://www.jongerenkooronevoice.nl/prayerrequests/all";
			List<PrayerRequest> apiPrayerRequests = RestHelper.GetRestDataFromUrl<List<PrayerRequest>>(query).Result;
			foreach (var pr in apiPrayerRequests)
			{
				if (pr.Id != 0)
				{
					App.Database.PrayerRequests.UpdateOrInsertByApiId(pr);
				}

			}
		}
	}
}
