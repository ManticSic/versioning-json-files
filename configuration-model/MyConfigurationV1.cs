using System;
using Newtonsoft.Json;

namespace configuration_model
{
    public class MyConfigurationV1 : MyConfiguration
    {
        public override string Version { get; } = "1";

        public string Name { get; set; } = String.Empty;

        public int MinValue { get; set; } = int.MinValue;

        public int MaxValue { get; set; } = int.MaxValue;

        public override MyConfiguration ToLatest()
        {
            MyConfigurationV2 nextConfiguration = new MyConfigurationV2
            {
                Name = Name,
                MinValue = MinValue,
                MaxValue = MaxValue
            };

            return nextConfiguration.ToLatest();
        }
    }
}