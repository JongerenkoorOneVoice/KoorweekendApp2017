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
    public class ChoirWeekendBaseAssignmentResult
    {
		/// <summary>
		/// Time in milliseconds
		/// </summary>
		/// <value>The used time.</value>
		public Int32 Time { get; set; }

		public Int32 Score { get; set; }
    }
}
