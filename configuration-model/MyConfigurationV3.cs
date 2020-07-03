using System;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

namespace configuration_model
{
    public class MyConfigurationV3 : MyConfiguration
    {
        public override string Version { get; } = "3";

        public string Name { get; set; } = String.Empty;

        public int Value { get; set; } = 0;

        public NestedObject Nested { get; set; } = new NestedObject();

        public override MyConfiguration ToLatest()
        {
            return this;
        }

        public sealed class NestedObject
        {
            public string Foo { get; set; } = "Bar";
        }
    }
}