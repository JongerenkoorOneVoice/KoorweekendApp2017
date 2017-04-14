using System;
using KoorweekendApp2017.Tasks;
using Xamarin.Forms;

namespace KoorweekendApp2017.Helpers
{
	public static class GlobalSettingsHelper
	{

		public static bool VersionSmallerThanAppVersion(string targetVersion)
		{
			var currentVersion = HardAppSettings.Version;

			if (String.IsNullOrEmpty(targetVersion)) return false;
			if (targetVersion == currentVersion) return false;

			var currentVersionArray = targetVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			var targetVersionArray = targetVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

			if (currentVersion.Length < 4 || targetVersion.Length < 4)
			{
				throw new Exception("A version number should contain a least 4 characters");
			}

			for (var i = 0; i < 4; i++)
			{
				var currentVersionPart = Convert.ToInt32(currentVersionArray[i]);
				var targetVersionPart = Convert.ToInt32(targetVersionArray[i]);

				if (currentVersionPart < targetVersionPart) return false;
			}

			return true;
		}

		public static bool VersionBiggerThanAppVersion(string targetVersion)
		{
			var currentVersion = HardAppSettings.Version;

			if (String.IsNullOrEmpty(targetVersion)) return false;
			if (targetVersion == currentVersion) return false;

			var currentVersionArray = targetVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			var targetVersionArray = targetVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

			if (currentVersion.Length < 4 || targetVersion.Length < 4)
			{
				throw new Exception("A version number should contain a least 4 characters");
			}

			for (var i = 0; i < 4; i++)
			{
				var currentVersionPart = Convert.ToInt32(currentVersionArray[i]);
				var targetVersionPart = Convert.ToInt32(targetVersionArray[i]);

				if (currentVersionPart > targetVersionPart) return false;
			}

			return true;
		}

		public static bool ShouldShowLatesReleaseNotes()
		{
			var loadUpdateOverview = false;
			             
			if (HardAppSettings.Version != GlobalSettings.LatestAppVersion && NetworkHelper.IsReachable("jongerenkooronevoice.nl"))
			{
				// Make sure I have the lates data from the api
				DataSync.UpdateGlobalSettingsInDbFromApi(shouldUpdateAll: true);
			}
		
			if (HardAppSettings.Version == GlobalSettings.LatestAppVersion)
			{
				loadUpdateOverview = true;
				var lastShownVersion = App.Database.Settings.GetValue<String>("releasenotesLastShownForVersion");
				if (!String.IsNullOrEmpty(lastShownVersion))
				{
					loadUpdateOverview = lastShownVersion != HardAppSettings.Version;
				}
			}
			return loadUpdateOverview;
		}
	}
}
