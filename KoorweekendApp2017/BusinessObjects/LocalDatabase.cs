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

		public LocalDatabase(SQLiteConnection database)
		{
			Database = database;
			Database.CreateTable<Contact>();

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
