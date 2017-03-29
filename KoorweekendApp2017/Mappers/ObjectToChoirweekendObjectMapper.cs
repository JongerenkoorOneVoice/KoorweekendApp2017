using System;
using KoorweekendApp2017.Models;
using Newtonsoft.Json;

namespace KoorweekendApp2017.Mappers
{
	public static class ObjectToChoirweekendObjectMapper
	{


		public static ChoirWeekendObject Map<T>(T original) where T : ChoirWeekendBaseObject
		{

			var type = typeof(T).Name;
			var id = String.Format("{0}_{1}", type, original.Id);
			var json = JsonConvert.SerializeObject(original);

			return new ChoirWeekendObject(id, type, json);

		}
	}
}
