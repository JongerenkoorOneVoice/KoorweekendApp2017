using System;
namespace KoorweekendApp2017
{
	public class DailyBreadVerse
	{

		public DateTime ts { get; set;}

		public String source { get; set; }

		public String fulltext { get; set; }

		public String wpId { get; set; }

		public DailyBreadStringsPerTranslation text { get; set; }

	}
}
