using System;
namespace KoorweekendApp2017
{
	public static class GlobalSettings
	{
		private static string _latestAppVersion { get; set; }

		private static string _minimumVersionToAllowRunning { get; set; }

		private static string _startupMessage { get; set; }

		private static string _currentVersionReleaseNotes { get; set; }

		public static void Reset()
		{
			_latestAppVersion = null;
			_minimumVersionToAllowRunning = null;
			_startupMessage = null;
			_currentVersionReleaseNotes = null;

		}

		public static string LatestAppVersion
		{
			get
			{
				if (!String.IsNullOrEmpty(_latestAppVersion))
				{
					return _latestAppVersion;
				}

				var globalSetting = App.Database.GlobalSettings.GetByKey("latestAppVersion");
				if (globalSetting != null)
				{
					_latestAppVersion = globalSetting.Value;
				}
				return _latestAppVersion;
			}
		}

		public static string MinimumVersionToAllowRunning
		{
			get
			{
				if (!String.IsNullOrEmpty(_latestAppVersion))
				{
					return _latestAppVersion;
				}

				var globalSetting = App.Database.GlobalSettings.GetByKey("minimumVersionToAllowRunning");
				if (globalSetting != null)
				{
					_latestAppVersion = globalSetting.Value;
				}
				return _latestAppVersion;
			}
		}

		public static string StartupMessage
		{
			get
			{
				if (!String.IsNullOrEmpty(_latestAppVersion))
				{
					return _latestAppVersion;
				}

				var globalSetting = App.Database.GlobalSettings.GetByKey("startupMessage");
				if (globalSetting != null)
				{
					_latestAppVersion = globalSetting.Value;
				}
				return _latestAppVersion;
			}
		}

		public static string CurrentVersionReleaseNotes
		{
			get
			{
				if (!String.IsNullOrEmpty(_latestAppVersion))
				{
					return _latestAppVersion;
				}

				var globalSetting = App.Database.GlobalSettings.GetByKey("currentVersionReleaseNotes");
				if (globalSetting != null)
				{
					_latestAppVersion = globalSetting.Value;
				}
				return _latestAppVersion;
			}
		}


	}
}
