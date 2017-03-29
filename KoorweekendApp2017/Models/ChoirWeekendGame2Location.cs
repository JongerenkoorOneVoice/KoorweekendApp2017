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
	public class ChoirWeekendGame2Location : ChoirWeekendBaseLocation
    {
		public String Image { get; set; }

		public String Code { get; set; }


    }
}
