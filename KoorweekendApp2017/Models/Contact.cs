using System;
using System.Globalization;
using SQLite;

namespace KoorweekendApp2017.Models
{
	public class Contact : DatabaseItemBase
	{

		public DateTime LastModified { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string AreaCode { get; set; }
		public string City { get; set; }
		public DateTime? BirthDate { get; set; }
		public DateTime? StartDate { get; set; }
		public string Phone1 { get; set; }
		public string Mobile1 { get; set; }
		public string Email1 { get; set; }
        public Boolean IsVisible { get; set; }

        [Ignore]
		public string FullName
		{
			get
			{
				return String.Format("{0} {1}", FirstName, LastName);
			}
		}

		[Ignore]
		public String BirthDateFormatted
		{
			get
			{
				string result = String.Empty;
				if (BirthDate != null)
				{
					result = ((DateTime)BirthDate).ToString("d MMMM yyyy", new CultureInfo("nl-NL"));
				}
				return result;
			}
		}

		[Ignore]
		public String StartDateFormatted
		{
			get
			{
				string result = String.Empty;
				if (BirthDate != null)
				{
					result = ((DateTime)StartDate).ToString("d MMMM yyyy", new CultureInfo("nl-NL"));
				}
				return result;
			}
		}


	}
}
