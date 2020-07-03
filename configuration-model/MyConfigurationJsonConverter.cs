using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace configuration_model
{
    public class MyConfigurationJsonConverter : JsonConverter
    {
        private static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new MyConfigurationConcreteClassConverter() };
        private static IDictionary<string, Type> derivedTypes = new Dictionary<string, Type>
        {
            {"1", typeof(MyConfigurationV1)},
            {"2", typeof(MyConfigurationV2)},
            {"3", typeof(MyConfigurationV3)}
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = value.GetType();

            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties();

            writer.WriteStartObject();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object[] jsonIgnoreAttributes = propertyInfo.GetCustomAttributes(typeof(JsonIgnoreAttribute), true);

                if (jsonIgnoreAttributes.Any())
                {
                    return;
                }

                writer.WritePropertyName(propertyInfo.Name);
                serializer.Serialize(writer, propertyInfo.GetValue(value));
                // writer.WriteValue();
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            if (!jObject.ContainsKey(nameof(MyConfiguration.Version)))
            {
                throw new JsonReaderException($"Json does not contain required key: {nameof(MyConfiguration.Version)}");
            }

            string version = jObject[nameof(MyConfiguration.Version)].Value<string>();

            if (!derivedTypes.ContainsKey(version))
            {
                throw new JsonReaderException($"Found unknown derived type {version}");
            }

            Type derivedType = derivedTypes[version];

            MyConfiguration loadedConfiguration = (MyConfiguration) JsonConvert.DeserializeObject(jObject.ToString(), derivedType, SpecifiedSubclassConversion);

            return loadedConfiguration.ToLatest();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MyConfiguration);
        }
    }
}