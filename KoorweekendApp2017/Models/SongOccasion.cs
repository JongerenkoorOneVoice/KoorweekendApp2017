using System;
using System.Globalization;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class SongOccasion : DatabaseItemBase
	{
		public string Title { get; set; }
		public string Description { get; set; }
        public Boolean IsVisible { get; set; }
    }
}
