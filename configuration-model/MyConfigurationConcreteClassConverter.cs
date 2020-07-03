using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace configuration_model
{
    public class MyConfigurationConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(MyConfiguration).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            {
                return null;
            }

            return base.ResolveContractConverter(objectType);
        }
    }
}