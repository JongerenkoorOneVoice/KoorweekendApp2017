using System;
using System.Collections.Generic;
using System.Linq;
using KoorweekendApp2017.Models;
using SQLite;

namespace KoorweekendApp2017.BusinessObjects
{
	public class LocalDatabase
	{
		private static SQLiteConnection Database { get; set;}

		public SettingsTable Settings { get; set;}

		public LocalDatabase(SQLiteConnection database)
		{
			Database = database;
			Database.CreateTable<Contact>();
			Database.CreateTable<Setting>();
			Database.CreateTable<Song>();
			//Database.CreateTable<Event>();
			Settings = new SettingsTable();
		}

		public class SettingsTable
		{
			public Setting GetByKey(string key)
			{
				return (from i in Database.Table<Setting>() where i.Key == key select i).ToList().FirstOrDefault();
			}
		}

		public IEnumerable<Contact> GetItems()
		{
			return (from i in Database.Table<Contact>() select i).ToList();
		}

		public Int32 InsertContact(Contact contact)
		{
			//Database.Insert(contact);
			return 0;
		}

		public Setting GetSettingByKey(string key)
		{
			return (from i in Database.Table<Setting>() where i.Key == key select i).ToList().FirstOrDefault();
		}

		public Int32 InsertSetting(Setting setting)
		{
			return Database.Insert(setting);
		}

		public Dictionary<String, Setting> GetAllSettings()
		{
			return (from i in Database.Table<Setting>() select i).ToDictionary(x=> x.Key, x=> x);
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
