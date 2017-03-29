using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoorweekendApp2017.Extensions
{
    public static class ObjectExtensions
    {
        public static String ToJSON(this Object obj)
        {
            List<string> propertiesToSerialize = new List<string>(new string[] { "EmpId", "Name", "Salary", "Department" });

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new IgnoreAttributesResolver(propertiesToSerialize);
            var test = JsonConvert.SerializeObject(obj, settings);
            return test;
        }

    }
}
