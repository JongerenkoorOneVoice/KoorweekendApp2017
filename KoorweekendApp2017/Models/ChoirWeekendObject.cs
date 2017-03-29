using System;
using SQLite;

namespace KoorweekendApp2017
{
	public class ChoirWeekendObject
	{
		[PrimaryKey]
		public String Id { get; set; }

		public String ObjectType { get; set; }

		public String Json { get; set; }

		public ChoirWeekendObject()
		{

		}

		public ChoirWeekendObject(String id, String objectType, String json )
		{
			Id = id;
			ObjectType = objectType;
			Json = json;
		}
	}

}
