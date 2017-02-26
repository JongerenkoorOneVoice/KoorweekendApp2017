using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Interfaces;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.BusinessObjects
{
	public class AppWebService
	{

		public ContactsEndpoint Contacts { get; set; }

		public SongsEndpoint Songs { get; set; }

		public EventsEndpoint Events { get; set; }

		public NewsEndpoint News { get; set; }

		public SongOccasionsEndpoint SongOccasions { get; set; }

		public PrayerRequestsEndpoint PrayerRequests { get; set;}

		public AppWebService()
		{
			Contacts = new ContactsEndpoint();
			Songs = new SongsEndpoint();
			Events = new EventsEndpoint();
			News = new NewsEndpoint();
			SongOccasions = new SongOccasionsEndpoint();
			PrayerRequests = new PrayerRequestsEndpoint();
		}



		public class ContactsEndpoint
		{

			public async Task<List<Contact>> GetAllContactsAsync()
			{
				string query = "http://www.jongerenkooronevoice.nl/contacts/all";
				return await RestHelper.GetRestDataFromUrl<List<Contact>>(query);
			}

			public async Task<Contact> GetContactByIdAsync(Int32 id)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/contacts/byid/{0}", id);
				return await RestHelper.GetRestDataFromUrl<Contact>(query);
			}

			public async Task<List<Contact>> GetContactsChangedAfterDateAsync(DateTime date)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/contacts/changedafter/{0}", date.ToString("yyyy-MM-dd"));
				return await RestHelper.GetRestDataFromUrl<List<Contact>>(query);
			}

		}

		public class SongsEndpoint
		{
			public async Task<List<Song>> GetAllSongsAsync()
			{
				string query = "http://www.jongerenkooronevoice.nl/songs/all";
				return await RestHelper.GetRestDataFromUrl<List<Song>>(query);
			}

			public async Task<Song> GetSongByIdAsync(Int32 id)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/songs/byid/{0}", id);
				return await RestHelper.GetRestDataFromUrl<Song>(query);
			}

			public async Task<List<Song>> GetSongsChangedAfterDateAsync(DateTime date)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/songs/changedafter/{0}", date.ToString("yyyy-MM-dd"));
				return await RestHelper.GetRestDataFromUrl<List<Song>>(query);
			}
		}

		public class EventsEndpoint
		{
			public async Task<List<Event>> GetAllEventsAsync()
			{
				string query = "http://www.jongerenkooronevoice.nl/apievents/all";
				return await RestHelper.GetRestDataFromUrl<List<Event>>(query);
			}

			public async Task<Event> GetEventByIdAsync(Int32 id)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/apievents/byid/{0}", id);
				return await RestHelper.GetRestDataFromUrl<Event>(query);
			}

			public async Task<List<Event>> GetEventsChangedAfterDateAsync(DateTime date)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/apievents/changedafter/{0}", date.ToString("yyyy-MM-dd"));
				return await RestHelper.GetRestDataFromUrl<List<Event>>(query);
			}
		}

		public class NewsEndpoint
		{
			public async Task<List<News>> GetAllNewsAsync()
			{
				string query = "http://www.jongerenkooronevoice.nl/apinews/all";
				return await RestHelper.GetRestDataFromUrl<List<News>>(query);
			}

			public async Task<News> GetNewsByIdAsync(Int32 id)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/apinews/byid/{0}", id);
				return await RestHelper.GetRestDataFromUrl<News>(query);
			}

			public async Task<List<News>> GetNewsChangedAfterDateAsync(DateTime date)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/apinews/changedafter/{0}", date.ToString("yyyy-MM-dd"));
				return await RestHelper.GetRestDataFromUrl<List<News>>(query);
			}
		}

		public class SongOccasionsEndpoint
		{
			public async Task<List<SongOccasion>> GetAllSongOccasionsAsync()
			{
				string query = "http://www.jongerenkooronevoice.nl/songoccasions/all";
				return await RestHelper.GetRestDataFromUrl<List<SongOccasion>>(query);
			}
		}

		public class PrayerRequestsEndpoint
		{
			public async Task<List<News>> GetAllPrayerRequestsAsync()
			{
				string query = "http://www.jongerenkooronevoice.nl/prayerrequests/all";
				return await RestHelper.GetRestDataFromUrl<List<News>>(query);
			}

			public async Task<News> GetPrayerRequestsByIdAsync(Int32 id)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/prayerrequests/byid/{0}", id);
				return await RestHelper.GetRestDataFromUrl<News>(query);
			}

			public async Task<List<News>> GetPrayerRequestsChangedAfterDateAsync(DateTime date)
			{
				string query = String.Format("http://www.jongerenkooronevoice.nl/prayerrequests/changedafter/{0}", date.ToString("yyyy-MM-dd"));
				return await RestHelper.GetRestDataFromUrl<List<News>>(query);
			}

			public async Task<PrayerRequest> PostPrayerRequestsAsync(PrayerRequest prayerRequest)
			{
				string query = "http://www.jongerenkooronevoice.nl/prayerrequests/createnew";
				return await RestHelper.PostDataToUrl<PrayerRequest>(query, prayerRequest);
			}

			public async Task<PrayerRequest> PutPrayerRequestsAsync(PrayerRequest prayerRequest)
			{
				string query = "http://www.jongerenkooronevoice.nl/prayerrequests/updatebyid/" + Convert.ToString(prayerRequest.Id);
				return await RestHelper.PutDataToUrl<PrayerRequest>(query, prayerRequest);
			}
		}

		public class ChoirWeekendEndpoint
		{
			public class PackingList
			{
				public async Task<List<ChoirWeekendPackingListItem>> GetAll()
				{
					string query = "http://www.jongerenkooronevoice.nl/choirweekends/packinglist";
					return await RestHelper.GetRestDataFromUrl<List<ChoirWeekendPackingListItem>>(query);
				}
			}

			public class Game1
			{
				public async Task<List<ChoirWeekendGame1Assignment>> GetAll()
				{
					string query = "http://www.jongerenkooronevoice.nl/choirweekends/game1assignments";
					return await RestHelper.GetRestDataFromUrl<List<ChoirWeekendGame1Assignment>>(query);
				}
			}

			public class Game2
			{
				public async Task<List<ChoirWeekendGame2Assignment>> GetAll()
				{
					string query = "http://www.jongerenkooronevoice.nl/choirweekends/game2assignments";
					return await RestHelper.GetRestDataFromUrl<List<ChoirWeekendGame2Assignment>>(query);
				}
			}
		}
	}
}
