using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using KoorweekendApp2017.Extensions;
using System.Globalization;

namespace KoorweekendApp2017.Models
{
    public class News : DatabaseItemBase
    {

        public DateTime LastModified { get; set; }
        public String Title { get; set; }
		public Boolean IsVisible { get; set;}
		public String HTML { get; set;}

		[Ignore]
		public String Text
		{
			get
			{
				return HTML.StripHTML();
			}
		}

		[Ignore]
		public String LastModifiedDateFormatted
		{
			get
			{
				return LastModified.ToString("dddd d MMMM yyyy", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String LastModifiedTimeFormatted
		{
			get
			{
				return LastModified.ToString("HH:mm", new CultureInfo("nl-NL"));
			}
		}

		[Ignore]
		public String LastModifiedDateAndTimeFormatted
		{
			get
			{
				return String.Format("Geupdate op {0} om {1}",LastModifiedDateFormatted, LastModifiedTimeFormatted);
			}
		}
    }
}
