using System.Collections.Generic;
using System.IO;
using System.Text;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Serializers
{
    [Subject(typeof(DefaultJsonSerializer))]
    public class DefaultJsonSerializerSpecification
    {
        static MemoryStream stream;

        static DefaultJsonSerializer serializer;

        Establish context = () =>
        {
            stream = new MemoryStream();
            serializer = new DefaultJsonSerializer();
        };

        public class when_deserializing_dynamic
        {
            static dynamic deserialized;

            Because of = () =>
                deserialized = serializer.Deserialize<dynamic>(new MemoryStream(Encoding.UTF8.GetBytes(@"{ ""message"": ""hi sir"" }")));

            It should_deserialize_items_when_array = () =>
                ((string)deserialized.message).ShouldEqual("hi sir");
        }

        class when_deserializing_array_with_default_settings
        {
            static List<string> deserialized;

            Establish context = () =>
            {
                serializer.SerializeAsync(stream, new[] { "a", "b", "c" }).Await();
                stream.Position = 0;
            };

            Because of = () =>
                deserialized = serializer.Deserialize<List<string>>(stream);

            It should_deserialize_items_when_array = () =>
                deserialized.Count.ShouldEqual(3);
        }

        class when_deserializing_object_with_default_settings
        {
            static Person deserialized;

            Establish context = () =>
            {
                serializer.SerializeAsync(stream, new Person { Name = "fred", Age = 30 }).Await();
                stream.Position = 0;
            };

            Because of = () =>
                deserialized = serializer.Deserialize<Person>(stream);

            It should_deserialize_items_when_array = () =>
                deserialized.Name.ShouldEqual("fred");
        }

        public class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
