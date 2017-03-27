using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Interfaces;
using KoorweekendApp2017.Mappers;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.BusinessObjects
{
	public class LocalDatabase : IOneVoiceDatabase
	{
		private static SQLiteConnection Database { get; set; }

		public SettingTable Settings { get; set; }

		public ContactTable Contacts { get; set; }

		public SongTable Songs { get; set; }

		public EventTable Events { get; set; }

		public LogItemTable LogItems { get; set; }

		public SongOccasionTable SongOccasions { get; set; }

		public PrayerRequestTable PrayerRequests { get; set; }

		public NewsTable News { get; set; }

		public ChoirWeekend2017Table ChoirWeekend2017 { get; set; }

		public LocalDatabase(SQLiteConnection database)
		{
			Database = database;
			Database.CreateTable<Contact>();
			Database.CreateTable<Setting>();
			Database.CreateTable<Song>();
			Database.CreateTable<Event>();
			Database.CreateTable<News>();
			Database.CreateTable<LogItem>();
			Database.CreateTable<SongOccasion>();
			Database.CreateTable<PrayerRequest>();
			// Choirweekend 2017
			Database.CreateTable<ChoirWeekendObject>();


			Settings = new SettingTable();
			Contacts = new ContactTable();
			Songs = new SongTable();
			Events = new EventTable();
			LogItems = new LogItemTable();
			SongOccasions = new SongOccasionTable();
			PrayerRequests = new PrayerRequestTable();
			News = new NewsTable();
			ChoirWeekend2017 = new ChoirWeekend2017Table();
		}

		public class SettingTable
		{
			public Setting GetByKey(string key)
			{
				return (from i in Database.Table<Setting>() where i.Key == key select i).ToList().FirstOrDefault();
			}

			public List<Setting> GetAll()
			{
				return (from i in Database.Table<Setting>() select i).ToList();
			}

			public void RemoveByKey(string key)
			{
				Setting tmpSetting = GetByKey(key);
				if (tmpSetting != null)
				{
					Database.Delete(tmpSetting);
				}

			}

			public void RemoveAll()
			{
				Database.DeleteAll<Setting>();
			}

			public void Set(string key, string value, bool parseJson = true)
			{
				Setting setting = GetByKey(key);
				if (setting == null)
				{
					setting = new Setting();
				}
				setting.Key = key;
				if (parseJson) setting.Value = JsonConvert.SerializeObject(value);
				else setting.Value = value;
				Database.InsertOrReplace(setting);
			}

			public void Set(string key, object o)
			{
				string value = JsonConvert.SerializeObject(o);
				Set(key, value, false);
			}

			public void Set(Setting setting)
			{
				Set(setting.Key, setting.Value);
			}

			public T GetValue<T>(string key)
			{
				Setting setting = GetByKey(key);
				if (setting != null)
				{
					return RestHelper.GetModelFromJson<T>(setting.Value);
				}
				return default(T);

			}
		}

		public class ContactTable
		{
			public Contact GetById(int id)
			{
				return (from i in Database.Table<Contact>() where i.Id == id select i).ToList().FirstOrDefault();
			}

			public List<Contact> GetAll()
			{
				return (from i in Database.Table<Contact>() select i).OrderBy(i => i.FirstName).ToList();
			}

			public void RemoveById(int id)
			{
				var contact = GetById(id);
				Database.Delete(contact);
			}

			public void UpdateOrInsert(Contact contact)
			{
				Database.InsertOrReplace(contact);
			}
		}

		public class SongOccasionTable
		{
			public SongOccasion GetById(int id)
			{
				return (from i in Database.Table<SongOccasion>() where i.Id == id select i).ToList().FirstOrDefault();
			}

			public List<SongOccasion> GetAll()
			{
				return (from i in Database.Table<SongOccasion>() select i).ToList();
			}

			public void RemoveById(int id)
			{
				var occasion = GetById(id);
				Database.Delete(occasion);
			}

			public void UpdateOrInsert(SongOccasion occasion)
			{
				Database.InsertOrReplace(occasion);
			}
		}

		public class SongTable
		{
			public Song GetById(int id)
			{
				return (from i in Database.Table<Song>() where i.Id == id select i).ToList().FirstOrDefault();
			}

			public List<Song> GetAll()
			{
				return (from i in Database.Table<Song>() select i).ToList();
			}

			public void RemoveById(int id)
			{
				var song = GetById(id);
				Database.Delete(song);
			}

			public void UpdateOrInsert(Song song)
			{
				Database.InsertOrReplace(song);
			}
		}

		public class EventTable
		{
			public Event GetById(int id)
			{
				Event eventItem = (from i in Database.Table<Event>() where i.Id == id select i).ToList().FirstOrDefault();
				return eventItem;
			}

			public List<Event> GetAll()
			{
				return (from i in Database.Table<Event>() select i).ToList();
			}

			public void RemoveById(int id)
			{
				var eventItem = GetById(id);
				Database.Delete(eventItem);
			}

			public void UpdateOrInsert(Event eventItem)
			{
				Database.InsertOrReplace(eventItem);
			}
		}

		public class LogItemTable
		{
			public LogItem GetById(int id)
			{
				return (from i in Database.Table<LogItem>() where i.Id == id select i).ToList().FirstOrDefault();
			}

			public List<LogItem> GetAll()
			{
				return (from i in Database.Table<LogItem>() select i).ToList();
			}

			public void RemoveById(int id)
			{
				var logItem = GetById(id);
				Database.Delete(logItem);
			}

			public void UpdateOrInsert(LogItem logItem)
			{
				Database.InsertOrReplace(logItem);
			}
		}

		public class NewsTable
		{
			public News GetById(int id)
			{
				return (from i in Database.Table<News>() where i.Id == id select i).ToList().FirstOrDefault();
			}

			public List<News> GetAll()
			{
				return (from i in Database.Table<News>() select i).OrderBy(i => i.LastModified).ToList();
			}

			public void UpdateOrInsert(News newsItem)
			{
				Database.InsertOrReplace(newsItem);
			}
		}

		public class PrayerRequestTable
		{
			public PrayerRequest GetById(int id)
			{
				return (from i in Database.Table<PrayerRequest>() where i.AppSpecificId == id select i).ToList().FirstOrDefault();
			}

			public PrayerRequest GetByApiId(int id)
			{
				return (from i in Database.Table<PrayerRequest>() where i.Id == id select i).ToList().FirstOrDefault();
			}

			public List<PrayerRequest> GetAll()
			{
				return (from i in Database.Table<PrayerRequest>() select i).ToList();
			}

			public void RemoveById(int id)
			{
				var prayerRequest = GetById(id);
				Database.Delete(prayerRequest);
			}

			public void UpdateOrInsert(PrayerRequest prayerRequest)
			{
				if (prayerRequest.ContactId == 0) throw new Exception("ContactId not set for PrayerRequest");
				var tmpRequest = GetById(Convert.ToInt32(prayerRequest.AppSpecificId));
				if (tmpRequest == null)
				{
					prayerRequest.DateCreatedInApp = DateTime.Now;
				}
				prayerRequest.LastModifiedInApp = DateTime.Now;
				Database.InsertOrReplace(prayerRequest);
			}

			public void UpdateOrInsertByApiId(PrayerRequest prayerRequest)
			{
				var tmpRequest = GetByApiId(prayerRequest.Id);
				if (tmpRequest == null)
				{
					prayerRequest.DateCreatedInApp = DateTime.Now;
				}
				else
				{
					prayerRequest.AppSpecificId = tmpRequest.AppSpecificId;
				}

				prayerRequest.LastModifiedInApp = DateTime.Now;
				Database.InsertOrReplace(prayerRequest);
			}
		}



		/*
		public IEnumerable<TodoItem> GetItemsNotDone()
		{
			return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		}
		public TodoItem GetItem(int id)
		{
			return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
		}
		public int DeleteItem(int id)
		{
			return database.Delete<TodoItem>(id);
		}
		*/



		public class ChoirWeekend2017Table
		{

			public Game1AssignmentTable Game1 = new Game1AssignmentTable();

			public Game2AssignmentTable Game2 = new Game2AssignmentTable();

			public PackinglistTable PackingList = new PackinglistTable();

			public class PackinglistTable
			{
				public ChoirWeekendPackingListItem GetById(String id)
				{
					var type = typeof(ChoirWeekendPackingListItem).Name;
					var tempId = String.Format("{0}_{1}", type, id);
					var item =  (from i in Database.Table<ChoirWeekendObject>() where i.Id == tempId select i).ToList().FirstOrDefault();
					if (item == null) return null;
					return JsonConvert.DeserializeObject<ChoirWeekendPackingListItem>(item.Json);
				}

				public List<ChoirWeekendPackingListItem> GetAll()
				{
					var type = typeof(ChoirWeekendPackingListItem).Name;
					var items = (from i in Database.Table<ChoirWeekendObject>() where i.ObjectType == type select i).ToList();
					if (items == null) return null;
					return items.Select((ChoirWeekendObject item) => {
						return JsonConvert.DeserializeObject<ChoirWeekendPackingListItem>(item.Json);
					}).OrderBy(i => i.Name).ToList();

				}

				public void UpdateOrInsert(ChoirWeekendPackingListItem packinglistItem)
				{
					Database.InsertOrReplace(ObjectToChoirweekendObjectMapper.Map(packinglistItem));
				}
			}

			public class Game1AssignmentTable
			{
				public ChoirWeekendGame1Assignment GetById(String id)
				{
					var type = typeof(ChoirWeekendGame1Assignment).Name;
					var tempId = String.Format("{0}_{1}", type, id);
					var item = (from i in Database.Table<ChoirWeekendObject>() where i.Id == tempId select i).ToList().FirstOrDefault();
					if (item == null) return null;
					return JsonConvert.DeserializeObject<ChoirWeekendGame1Assignment>(item.Json);
				}

				public List<ChoirWeekendGame1Assignment> GetAll()
				{
					var type = typeof(ChoirWeekendGame1Assignment).Name;
					var items = (from i in Database.Table<ChoirWeekendObject>() where i.ObjectType == type select i).ToList();
					if (items == null) return null;

					/*
					return items.Select((ChoirWeekendObject item) =>
					{
						return JsonConvert.DeserializeObject<ChoirWeekendGame1Assignment>(item.Json);
					}).OrderBy(i => i.Settings.ConsecutionIndex).ToList();
					*/

					// hardcoded test values
					return new List<ChoirWeekendGame1Assignment>()
					{
						new ChoirWeekendGame1Assignment()
						{
							Location = new ChoirWeekendGame1Location()
							{
								Position = new ChoirWeekendBasePosition()
								{
									Longitude = 4.578520,
									Lattitude = 51.897761
								},
								Name = "Locatie 1",
								Description = "Prins Claus straat"

							}
						},
						new ChoirWeekendGame1Assignment()
						{
							Location = new ChoirWeekendGame1Location()
							{
								Position = new ChoirWeekendBasePosition()
								{
									Longitude = 4.604537299999947,
									Lattitude = 51.8827908
								},
								Name = "Locatie 2",
								Description = "De Kern"

							}
						},
						new ChoirWeekendGame1Assignment()
						{
							Location = new ChoirWeekendGame1Location()
							{
								Position = new ChoirWeekendBasePosition()
								{
									Longitude = 4.577484899999945,
									Lattitude = 51.8981004
								},
								Name = "Locatie 3",
								Description = "Thuis"

							}
						},
						new ChoirWeekendGame1Assignment()
						{
							Location = new ChoirWeekendGame1Location()
							{
								Position = new ChoirWeekendBasePosition()
								{
									Longitude = 4.577734,
									Lattitude = 51.899459
								},
								Name = "Locatie 4",
								Description = "Einde van de straat"

							}
						},
						new ChoirWeekendGame1Assignment()
						{
							Location = new ChoirWeekendGame1Location()
							{
								Position = new ChoirWeekendBasePosition()
								{
									Longitude = 4.567674,
									Lattitude = 51.900058
								},
								Name = "Locatie 5",
								Description = "Richard"

							}
						}
					};
				}

				public void UpdateOrInsert(ChoirWeekendGame1Assignment assignment)
				{
					Database.InsertOrReplace(ObjectToChoirweekendObjectMapper.Map(assignment));
				}
			}

			public class Game2AssignmentTable
			{
				public ChoirWeekendGame2Assignment GetById(String id)
				{
					var type = typeof(ChoirWeekendGame2Assignment).Name;
					var tempId = String.Format("{0}_{1}", type, id);
					var item = (from i in Database.Table<ChoirWeekendObject>() where i.Id == tempId select i).ToList().FirstOrDefault();
					if (item == null) return null;
					return JsonConvert.DeserializeObject<ChoirWeekendGame2Assignment>(item.Json);
				}

				public List<ChoirWeekendGame2Assignment> GetAll()
				{
					var type = typeof(ChoirWeekendGame2Assignment).Name;
					var items = (from i in Database.Table<ChoirWeekendObject>() where i.ObjectType == type select i).ToList();
					if (items == null) return null;
					return items.Select((ChoirWeekendObject item) =>
					{
						return JsonConvert.DeserializeObject<ChoirWeekendGame2Assignment>(item.Json);
					}).OrderBy(i => i.Settings.ConsecutionIndex).ToList();
				}

				public void UpdateOrInsert(ChoirWeekendGame2Assignment assignment)
				{
					Database.InsertOrReplace(ObjectToChoirweekendObjectMapper.Map(assignment));
				}
			}
		}
	}
}
