using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KoorweekendApp2017
{
	public partial class ToLowAppVersion : ContentPage
	{
		public ToLowAppVersion()
		{
			InitializeComponent();


			var currentVersion = HardAppSettings.Version;
			var latestVersion = GlobalSettings.LatestAppVersion;
			updateText.Text = String.Format("Je gebruikt een teveel verouderde versie van de koorapp. Je kan de app pas weer gebruiken als je hem geupdated hebt.\r\n\r\nHuidige versie: {0}\r\nLaatste versie: {1}", currentVersion, latestVersion);
		}
	}
}
