using System;

using KoorweekendApp2017.Interfaces;
using SQLite;

namespace KoorweekendApp2017
{
	public class DatabaseItemBase : IDatabaseItem
	{

		public DatabaseItemBase() { }

		[PrimaryKey]
		public int Id { get; set; }


	}
}
