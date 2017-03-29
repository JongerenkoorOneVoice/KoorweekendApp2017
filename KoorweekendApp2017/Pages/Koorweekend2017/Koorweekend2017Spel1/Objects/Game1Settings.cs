using System;
namespace KoorweekendApp2017
{
	public class Game1Settings
	{
		public Boolean CompassIsSupported { get; set; }

		public Boolean GpsAvailable { get; set; }

		public Boolean GpsEnabled { get; set; }

		public Boolean ShouldRunCheckAgain { get; set; }

		public Game1Settings()
		{
			ShouldRunCheckAgain = false;

		}
	}
}
