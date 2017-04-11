using System;
using Xamarin.Forms;

namespace KoorweekendApp2017.Helpers
{
	public static class SettingsHelper
	{

		public static void TogglePullToRefresh(ListView listview)
		{
			var allowedToConnect = NetworkHelper.IsReachable("jongerenkooronevoice.nl");
			listview.IsPullToRefreshEnabled = allowedToConnect;
		}
	}
}
