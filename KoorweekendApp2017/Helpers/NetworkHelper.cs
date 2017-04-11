using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using KoorweekendApp2017.Extensions;
using System.Text;
using XLabs.Platform.Services;
using XLabs.Ioc;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using KoorweekendApp2017.Models;

namespace KoorweekendApp2017.Helpers
{
	public static class NetworkHelper
	{

		public static Boolean IsReachable(String domain)
		{
			
			bool isReachable = false;
			bool permissionToConnect = true;
			var onlySyncOnWiFi = false;
			Setting onlySyncOnWifiSetting = App.Database.Settings.GetByKey("onlySyncOnWifi");
			if (onlySyncOnWifiSetting != null)
			{
				onlySyncOnWiFi = JsonConvert.DeserializeObject<Boolean>(onlySyncOnWifiSetting.Value);
				if (onlySyncOnWiFi)
				{
					var hasWiFi = App.Network.ConnectionTypes.Contains(ConnectionType.WiFi);
					permissionToConnect = hasWiFi;
				}
			}
				

			if (App.Network.IsConnected)
			{
				

				var task = Task.Run(async () =>
				{
					return await App.Network.IsReachable(domain).ConfigureAwait(false);
				});
				task.ConfigureAwait(false);
				isReachable = task.Result;
			}

            isReachable = true;
            bool hasInternet = CrossConnectivity.Current.IsConnected && isReachable;
			return hasInternet && permissionToConnect;

		}
	}
}
