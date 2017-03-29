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
	public class ChoirWeekendPackingListItem : ChoirWeekendBaseObject
	{

		public String Name { get; set; }
		public String Description { get; set; }

		[Ignore]
		private Boolean _isPacked { get; set; }

		public Boolean IsPacked { 
			get
			{
				return _isPacked;
			}
			set
			{
				_isPacked = value;
				if (_isPacked)
				{
					ToggledImage = "Agenda.png";
				}
				else
				{
					ToggledImage = "Home.png";
				}
			}
	    }

		[Ignore]
		public String ToggledImage { get; set; }
			
		
    }
}
