using System.Collections.Generic;
using System.IO;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Serializers
{
    [Subject(typeof(DefaultJsonSerializer))]
    public class DefaultJsonSerializerSpecification : WithSubject<DefaultJsonSerializer>
    {
        static MemoryStream stream;

        Establish context = () =>
            stream = new MemoryStream();


        // [Subject(typeof(DefaultJsonSerializer))]
        // public class when_deserializing_dynamic : WithSubject<DefaultJsonSerializer>
        // {
        //     Establish context = () =>
        //         json = SimpleJson.SerializeObject(new { message = "hi sir" });

        //     Because of = () =>
        //         deserialized = Subject.DeserializeString<dynamic>(json);

        //     It should_deserialize_items_when_array = () =>
        //         ((string)deserialized.message).ShouldEqual("hi sir");

        //     static string json;

        //     static dynamic deserialized;
        // }

        class when_deserializing_array_with_default_settings
        {
            Establish context = () =>
            {
                Subject.Serialize(stream, new[] { "a", "b", "c" });
                stream.Position = 0;
            };

            Because of = () =>
                deserialized = Subject.Deserialize<List<string>>(stream);

            It should_deserialize_items_when_array = () =>
                deserialized.Count.ShouldEqual(3);

            static List<string> deserialized;
        }

        class when_deserializing_object_with_default_settings
        {
            Establish context = () =>
            {
                Subject.Serialize(stream, new Person { Name = "fred", Age = 30 });
                stream.Position = 0;
            };

            Because of = () =>
                deserialized = Subject.Deserialize<Person>(stream);

            It should_deserialize_items_when_array = () =>
                deserialized.Name.ShouldEqual("fred");

            static Person deserialized;
        }

        public class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        public class Container<T>
        {
            public T Root { get; set; }
        }
    }
}
