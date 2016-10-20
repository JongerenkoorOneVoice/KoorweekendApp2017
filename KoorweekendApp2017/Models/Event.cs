using System;
using System.Collections.Generic;

namespace KoorweekendApp2017.Models
{
	public class Event: DatabaseItemBase
	{

		public string LastModified { get; set; }
		public string StartDate { get; set; }
		public string StartTime { get; set; }
		public string EndDate { get; set; }
		public string EndTime { get; set; }
		public string Title { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string AreaCode { get; set; }
		public List<int> Songs { get; set; }



	}
}
