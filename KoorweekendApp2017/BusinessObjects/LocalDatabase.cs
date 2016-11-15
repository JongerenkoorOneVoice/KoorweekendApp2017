using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Helpers;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.BusinessObjects
{
	public class LocalDatabase
	{
		private static SQLiteConnection Database { get; set;}

		public SettingTable Settings { get; set; }

		public ContactTable Contacts { get; set; }

		public SongTable Songs { get; set; }

		public EventTable Events { get; set; }

		public LogItemTable LogItems { get; set; }

		public LocalDatabase(SQLiteConnection database)
		{
			Database = database;
			Database.CreateTable<Contact>();
			Database.CreateTable<Setting>();
			Database.CreateTable<Song>();
			Database.CreateTable<Event>();
			Database.CreateTable<LogItem>();
			Settings = new SettingTable();
			Contacts = new ContactTable();
			Songs = new SongTable();
			Events = new EventTable();
			LogItems = new LogItemTable();
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
				Database.Delete(tmpSetting);

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
				return (from i in Database.Table<Contact>() select i).ToList();
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

	}
}
