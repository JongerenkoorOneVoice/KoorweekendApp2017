﻿using System;
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
		public static void RunAllTasksInBackground()
		{

			MessagingCenter.Send(new StartApiContactSyncMessage(), "StartApiContactSyncMessage");
			MessagingCenter.Send(new StartApiSongSyncMessage(), "StartApiSongSyncMessage");
			MessagingCenter.Send(new StartApiEventSyncMessage(), "StartApiEventSyncMessage");
			MessagingCenter.Send(new StartApiSongOccasionSyncMessage(), "StartApiSongOccasionSyncMessage");
			MessagingCenter.Send(new StartApiNewsSyncMessage(), "StartApiNewsSyncMessage");
			MessagingCenter.Send(new StartApiPrayerRequestSyncMessage(), "StartApiPrayerRequestSyncMessage");
		
		}

		public static void RunAllTasksAndWaitForReady(bool downloadAll = false)
		{

			UpdateContactsInDbFromApi(downloadAll);
			UpdateSongsInDbFromApi(downloadAll);
			UpdateEventsInDbFromApi(downloadAll);
			UpdateSongOccasionsInDbFromApi(downloadAll);
			UpdateNewsInDbFromApi(downloadAll);
			SyncPrayerRequests(downloadAll);

		}

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
			bool isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (isAuthenticated)
			{

				DateTime lastUpdate = DateTime.Parse("1010-01-01");
				String lastUpdatedString = App.Database.Settings.GetValue<String>("lastContactsUpdate");
				if (!String.IsNullOrEmpty(lastUpdatedString) && !shouldUpdateAll)
				{
					lastUpdate = DateTime.Parse(lastUpdatedString);
					lastUpdate = lastUpdate.AddDays(-1);
				}

				List<Contact> contacts = App.AppWebService.Contacts.GetContactsChangedAfterDateAsync(lastUpdate).Result;
					
				if (contacts != null)
				{
					foreach (Contact contact in contacts)
					{
						App.Database.Contacts.UpdateOrInsert(contact);
					}
				}
				App.Database.Settings.Set("lastContactsUpdate", DateTime.Now.ToString());

			}
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
			bool isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (isAuthenticated)
			{
				DateTime lastUpdate = DateTime.Parse("1010-01-01");
				String lastUpdatedString = App.Database.Settings.GetValue<String>("lastSongUpdate");
				if (!String.IsNullOrEmpty(lastUpdatedString) && !shouldUpdateAll)
				{
					lastUpdate = DateTime.Parse(lastUpdatedString);
					lastUpdate = lastUpdate.AddDays(-1);
				}

				List<Song> songs = App.AppWebService.Songs.GetSongsChangedAfterDateAsync(lastUpdate).Result;
				if (songs != null)
				{
					foreach (Song song in songs)
					{
						App.Database.Songs.UpdateOrInsert(song);

					}
				}
				App.Database.Settings.Set("lastSongUpdate", DateTime.Now.ToString());
			}
			//	}
			
			//}
		}

		public static void UpdateEventsInDbFromApi(bool shouldUpdateAll = false)
		{
			bool isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (isAuthenticated)
			{
				DateTime lastUpdate = DateTime.Parse("1010-01-01");
				String lastUpdatedString = App.Database.Settings.GetValue<String>("lastEventsUpdate");
				if (!String.IsNullOrEmpty(lastUpdatedString) && !shouldUpdateAll)
				{
					lastUpdate = DateTime.Parse(lastUpdatedString);
					lastUpdate = lastUpdate.AddDays(-1);
				}

				List<Event> events = App.AppWebService.Events.GetEventsChangedAfterDateAsync(lastUpdate).Result;
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

		public static void UpdateNewsInDbFromApi(bool shouldUpdateAll = false)
		{
			bool isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (isAuthenticated)
			{
				DateTime lastUpdate = DateTime.Parse("1010-01-01");
				String lastUpdatedString = App.Database.Settings.GetValue<String>("lastNewsUpdate");
				if (!String.IsNullOrEmpty(lastUpdatedString) && !shouldUpdateAll)
				{
					lastUpdate = DateTime.Parse(lastUpdatedString);
					lastUpdate = lastUpdate.AddDays(-1);
				}

				List<News> newsItems = App.AppWebService.News.GetNewsChangedAfterDateAsync(lastUpdate).Result;
				if (newsItems != null)
				{
					foreach (News newsItem in newsItems)
					{
						App.Database.News.UpdateOrInsert(newsItem);

					}
				}
				App.Database.Settings.Set("lastNewsUpdate", DateTime.Now.ToString());
			}
		}

		public static void UpdateSongOccasionsInDbFromApi(bool shouldUpdateAll = false)
		{
			bool isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (isAuthenticated)
			{
				List<SongOccasion> occasions = App.AppWebService.SongOccasions.GetAllSongOccasionsAsync().Result;
				if (occasions != null)
				{
					foreach (SongOccasion occasion in occasions)
					{
						App.Database.SongOccasions.UpdateOrInsert(occasion);

					}
				}
				App.Database.Settings.Set("lastSongOccasionsUpdate", DateTime.Now.ToString());
			}
		}

		public static void UpdatePrayerRequestsInDbFromApi(bool shouldUpdateAll = false)
		{

			Int32 currentContactId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");

			string query = String.Empty;
			if (shouldUpdateAll)
			{
				query = "http://www.jongerenkooronevoice.nl/prayerrequests/all";
			}
			else
			{
				DateTime lastUpdate = DateTime.Parse("1010-01-01");
				String lastUpdatedString = App.Database.Settings.GetValue<String>("lastPrayerRequestSync");
				if (!String.IsNullOrEmpty(lastUpdatedString) && !shouldUpdateAll)
				{
					lastUpdate = DateTime.Parse(lastUpdatedString);
					lastUpdate = lastUpdate.AddDays(-1);
				}


				query = String.Format("http://www.jongerenkooronevoice.nl/prayerrequests/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));

			}

			List<PrayerRequest> apiPrayerRequests = RestHelper.GetRestDataFromUrl<List<PrayerRequest>>(query).Result;
            if (apiPrayerRequests != null)
            {
                foreach (var pr in apiPrayerRequests)
                {

                    bool endDateIsInPast = Convert.ToDateTime(pr.EndDate).Date < DateTime.Now;

                    if (pr.ContactId != currentContactId)
                    {
                        if (!pr.IsVisible || pr.IsPrivate || endDateIsInPast)
                        {
                            var tmp = App.Database.PrayerRequests.GetByApiId(pr.Id);
                            if (tmp != null) App.Database.PrayerRequests.RemoveById(Convert.ToInt32(tmp.AppSpecificId));
                        }
                    }

                    App.Database.PrayerRequests.UpdateOrInsertByApiId(pr);

                }
            }
			App.Database.Settings.Set("lastPrayerRequestSync", DateTime.Now.ToString());
		}


		public static void SyncPrayerRequests(bool shouldUpdateAll = false)
		{
            //string query = String.Format("http://www.jongerenkooronevoice.nl/prayerrequests/changedafter/{0}-{1}-{2}", lastUpdate.ToString("yyyy"), lastUpdate.ToString("MM"), lastUpdate.ToString("dd"));
            // Get all prayerrequests from the web api
            bool isAuthenticated = Task.Run(AuthenticationHelper.IsAuthenticated).Result;
			if (isAuthenticated)
			{
				DateTime lastUpdate = App.Database.Settings.GetValue<DateTime>("lastPrayerRequestSync");


				Int32 statusCount = 0;

				Int32 currentContactId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");

				List<PrayerRequest> appPrayerRequests = App.Database.PrayerRequests.GetAll();
				List<PrayerRequest> currentContactPrayerRequests = appPrayerRequests.FindAll(x => x.ContactId == currentContactId);

				// First write all users own data to the api, then redownload all data from the api.
                if (currentContactPrayerRequests.Count != 0)
                {
                    foreach (var pr in currentContactPrayerRequests)
                    {
						// Check if this prayerRequest was already updated in previous sync.
						if (pr.LastModifiedInApi < lastUpdate)
						{
							statusCount = statusCount + 1;
							if (statusCount == currentContactPrayerRequests.Count)
							{
								UpdatePrayerRequestsInDbFromApi();
							}
							continue;
						}


                        if (pr.Id == 0)
                        {
							App.AppWebService.PrayerRequests.PostPrayerRequestsAsync(pr).ContinueWith((arg) =>
                            {
                                PrayerRequest result = arg.Result;
                                result.AppSpecificId = pr.AppSpecificId;
                                App.Database.PrayerRequests.UpdateOrInsert(result);
                                statusCount = statusCount+1;
                                if (statusCount == currentContactPrayerRequests.Count)
                                {
                                    UpdatePrayerRequestsInDbFromApi();
                                }
                            });
                        }
                        else
                        {
							App.AppWebService.PrayerRequests.PutPrayerRequestsAsync(pr).ContinueWith((arg) =>
                            {
                                PrayerRequest result = arg.Result;
                                result.AppSpecificId = pr.AppSpecificId;
                                App.Database.PrayerRequests.UpdateOrInsert(arg.Result);
                                statusCount = statusCount+1;
                                if (statusCount == currentContactPrayerRequests.Count)
                                {
                                    UpdatePrayerRequestsInDbFromApi();
                                }
                            });
                        }
                    }
                }
                else
                {
                    UpdatePrayerRequestsInDbFromApi();
                }

			}
		}
	}
}
