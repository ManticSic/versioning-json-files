using Newtonsoft.Json;

namespace configuration_model
{
    [JsonConverter(typeof(MyConfigurationJsonConverter))]
    public abstract class MyConfiguration
    {
        public abstract string Version { get; }

        public abstract MyConfiguration ToLatest();
    }
}