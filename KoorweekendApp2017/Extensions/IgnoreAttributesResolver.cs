using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoorweekendApp2017.Extensions
{
    public class IgnoreAttributesResolver : DefaultContractResolver
    {

            private IList<string> attributesList = null;
            public IgnoreAttributesResolver(IList<string> propertiesToSerialize)
            {
                //attributesList = propertiesToSerialize;
            }

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
                if(attributesList != null) return properties.Where(p => attributesList.Contains(p.PropertyName)).ToList();
                return properties.ToList();
            }
        }

}
