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
    public class ChoirWeekendBaseAssignmentSettings
    {
		public Boolean IsVisible { get; set;}

		/// <summary>
		/// Time in milliseconds
		/// </summary>
		/// <value>The max time.</value>
		public Int32 MaxTime { get; set; }

		public Int32 MaxScore { get; set; }

		public Int32 ConsecutionIndex { get; set; }

		public Boolean IsBonus { get; set; }

		public String Description { get; set; }


    }
}
