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
	public class ChoirWeekendGame2Question
    {
		public String Image { get; set; }
		public String Question { get; set; }
	    public String Answer { get; set; }
		public List<String> MultipleChoiceAnswers { get; set; }
	    public Boolean IsOpenQuestion { get; set; }
	    public Boolean IsMultipleChoice { get; set; }

    }
}
