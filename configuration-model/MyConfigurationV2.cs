using System;
using Newtonsoft.Json;

namespace configuration_model
{
    public class MyConfigurationV2 : MyConfiguration
    {
        public override string Version { get; } = "2";

        public string Name { get; set; } = String.Empty;

        public int MinValue { get; set; } = int.MinValue;

        public int MaxValue { get; set; } = int.MaxValue;

        public int Value { get; set; } = 0;

        public override MyConfiguration ToLatest()
        {
            MyConfigurationV3 nextConfiguration = new MyConfigurationV3
            {
                Name = Name,
                Value = Value
            };

            return nextConfiguration.ToLatest();
        }
    }
}