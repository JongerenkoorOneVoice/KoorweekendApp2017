using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class PrayerRequest
	{
		[PrimaryKey, AutoIncrement]
		public Int32? AppSpecificId { get; set;}

		public Int32 Id { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public DateTime? DateCreatedInApi { get; set; }
        public DateTime? EndDate { get; set; }
        public Int32 ContactId { get; set; }
        public Boolean IsAnonymous { get; set; }
        public DateTime? DateCreatedInApp { get; set; }
        public Boolean IsVisible { get; set; }
        public Boolean IsPrivate { get; set; }
        public DateTime? LastModifiedInApi { get; set; }
        public DateTime? LastModifiedInApp { get; set; }

	}
}
