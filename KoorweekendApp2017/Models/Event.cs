using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class Event: DatabaseItemBase
	{

		public DateTime LastModified { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime EndTime { get; set; }
		public string Title { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string AreaCode { get; set; }

		[Ignore]
		public List<int> Songs { get; set; }

		[Ignore]
		public List<Song> SongItems { get; set; }

		public String SongsIds
		{
			get
			{
				return JsonConvert.SerializeObject(Songs);
			}
			set
			{
				Songs = JsonConvert.DeserializeObject<List<int>>(value);
			}
		
		}

		[Ignore]
		public String StartDateFormatted
		{
			get
			{
				return StartDate.ToString("dddd d MMMM yyyy", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String StartTimeFormatted
		{
			get
			{
				return StartTime.ToString("hh:mm", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String EndDateFormatted
		{
			get
			{
				return EndDate.ToString("dddd d MMMM yyyy", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String EndTimeFormatted
		{
			get
			{
				return StartDate.ToString("hh:mm", new CultureInfo("nl-NL"));
			}
		}

	}
}
