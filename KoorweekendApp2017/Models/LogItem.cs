using System;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class LogItem
	{
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
		public DateTime Time {get; set;}
		public String Title { get; set;}
		public String Description { get; set;}
		public String ExceptionDescription { get; set;}
		public String ExceptionStackTrace { get; set;}

	}
}
