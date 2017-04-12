using System;
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
	}
}
