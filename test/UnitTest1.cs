using System.IO;
using configuration_model;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadV1()
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader file = File.OpenText(@"Resources/v1.json"))
            {
                object config = serializer.Deserialize(file, typeof(MyConfiguration));
                Assert.IsInstanceOf<MyConfigurationV3>(config);
            }
        }

        [Test]
        public void ReadV2()
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader file = File.OpenText(@"Resources/v2.json"))
            {
                object config = serializer.Deserialize(file, typeof(MyConfiguration));
                Assert.IsInstanceOf<MyConfigurationV3>(config);
            }
        }

        [Test]
        public void ReadV3()
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader file = File.OpenText(@"Resources/v3.json"))
            {
                object config = serializer.Deserialize(file, typeof(MyConfiguration));
                Assert.IsInstanceOf<MyConfigurationV3>(config);
            }
        }

        [Test]
        public void WriteV1()
        {
            MyConfigurationV1 config = new MyConfigurationV1();

            string expected = "{\"Version\":\"1\",\"Name\":\"\",\"MinValue\":-2147483648,\"MaxValue\":2147483647}";

            string json = JsonConvert.SerializeObject(config);

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void WriteV2()
        {
            MyConfigurationV2 config = new MyConfigurationV2();

            string expected =
                "{\"Version\":\"2\",\"Name\":\"\",\"MinValue\":-2147483648,\"MaxValue\":2147483647,\"Value\":0}";

            string json = JsonConvert.SerializeObject(config);

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void WriteV3()
        {
            MyConfigurationV3 config = new MyConfigurationV3();

            string expected = "{\"Version\":\"3\",\"Name\":\"\",\"Value\":0,\"Nested\":{\"Foo\":\"Bar\"}}";

            string json = JsonConvert.SerializeObject(config);

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void WriteAndReadV1()
        {
            string expectedName = "Hello World";
            MyConfiguration config = new MyConfigurationV1 { Name = expectedName};

            string json = JsonConvert.SerializeObject(config);

            object result = JsonConvert.DeserializeObject(json, typeof(MyConfiguration));

            Assert.IsInstanceOf<MyConfigurationV3>(result);

            Assert.AreEqual(expectedName, ((MyConfigurationV3) result).Name);
        }

        [Test]
        public void WriteAndReadV2()
        {
            string expectedName = "Hello World";
            MyConfiguration config = new MyConfigurationV2 { Name = expectedName};

            string json = JsonConvert.SerializeObject(config);

            object result = JsonConvert.DeserializeObject(json, typeof(MyConfiguration));

            Assert.IsInstanceOf<MyConfigurationV3>(result);

            Assert.AreEqual(expectedName, ((MyConfigurationV3) result).Name);
        }

        [Test]
        public void WriteAndReadV3()
        {
            string expectedName = "Hello World";
            MyConfiguration config = new MyConfigurationV3 { Name = expectedName};

            string json = JsonConvert.SerializeObject(config);

            object result = JsonConvert.DeserializeObject(json, typeof(MyConfiguration));

            Assert.IsInstanceOf<MyConfigurationV3>(result);

            Assert.AreEqual(expectedName, ((MyConfigurationV3) result).Name);
        }
    }
}