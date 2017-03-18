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

namespace KoorweekendApp2017.Helpers
{
	public static class NetworkHelper
	{

		public static Boolean InternetConnected()
		{
			bool hasInternet = false;
			if (App.Network.IsConnected)
			{

				var task = Task.Run(async () =>
				{
					return await App.Network.IsReachable("jongerenkooronevoice.nl").ConfigureAwait(false);
				});

				hasInternet = task.Result;
			}
			return hasInternet;

		}
	}
}
