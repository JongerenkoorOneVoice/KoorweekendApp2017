using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class Event : DatabaseItemBase
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
		private List<int> _songs {get; set;}

		[Ignore]
		public List<int> Songs
		{
			get
			{
				return _songs;
			}
			set
			{
				_songs = value;
			}
		}

		[Ignore]
		private List<Song> _songItems { get; set; }

		[Ignore]
		public List<Song> SongItems
		{
			get
			{
				if (_songItems == null)
				{
					throw new Exception("SongItems must be required from de database manually");
				}
				return _songItems;
			}
			set
			{
				_songItems = value;
				_songs = _songItems.Select(x => x.Id).ToList();

			}
		}

		public String SongsIds
		{
			get
			{
				return JsonConvert.SerializeObject(_songs);
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
				{

					_songs = JsonConvert.DeserializeObject<List<int>>(value);
				}
				else {
					throw new Exception("The json value for SongIds is invalid");
				}
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
		public String StartDateFormattedWithLinebreaks
		{
			get
			{
				return StartDate.ToString("dddd\r\nd MMM yyyy", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String StartTimeFormatted
		{
			get
			{
				return StartTime.ToString("HH:mm", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String EndDateFormattedWithLinebreaks
		{
			get
			{
				return EndDate.ToString("dddd\r\nd MMM yyyy", new CultureInfo("nl-NL"));
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
				return EndTime.ToString("HH:mm", new CultureInfo("nl-NL"));
			}
		}

	}
}
