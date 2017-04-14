using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KoorweekendApp2017.Interfaces;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class GlobalSetting : IDatabaseItem
	{		
		[Unique]
		public int Id { get; set; }

		[PrimaryKey][Unique]
		public string Key { get; set; }

		public string Value { get; set; }
	}
}
