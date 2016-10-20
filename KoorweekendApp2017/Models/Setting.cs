using System;
using KoorweekendApp2017.Interfaces;
using SQLite;
using Xamarin.Forms;

namespace KoorweekendApp2017.Models
{
	public class Setting
	{
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }

		public String Key { get; set; }

		public String Value { get; set; }

	}
}

