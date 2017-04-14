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
	public static class UserHelper
	{

		public static Boolean CurrentUserIsDeveloper()
		{
			Int32 currentUserId = App.Database.Settings.GetValue<Int32>("authenticatedContactId");
			if (currentUserId == 0)
				currentUserId = AuthenticationHelper.GetAndWriteCurrentAuthenticatedUserIdToDb();
			
			if (currentUserId == 668 || currentUserId == 667 || currentUserId == 696)
				return true;

			return false;

		}
	}
}
