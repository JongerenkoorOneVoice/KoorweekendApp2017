using System;
using KoorweekendApp2017.BusinessObjects;
using Xamarin.Forms;
using SQLite;

namespace KoorweekendApp2017
{
	public static class UpdateHelper
	{
		public static void UpdateDatabaseAfterUpdate()
		{
			var db = DependencyService.Get<ISQLite>().GetConnection();
			db.DropTable<ChoirWeekendObject>();

			//App.Database.Settings.Set("lastPackinglistUpdate", DateTime.Now.ToString());
			//App.Database.Settings.Set("lastGame2Update", DateTime.Now.ToString());
			App.Database.Settings.Set("lastGame1Update", DateTime.Now.ToString());
		}
	}

	public class ChoirWeekendObject
	{
		[PrimaryKey]
		public String Id { get; set; }

		public String ObjectType { get; set; }

		public String Json { get; set; }

		public ChoirWeekendObject()
		{

		}

		public ChoirWeekendObject(String id, String objectType, String json)
		{
			Id = id;
			ObjectType = objectType;
			Json = json;
		}
	}

}
