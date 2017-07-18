using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Serializers
{
    public class DefaultJsonSerializerSpecification
    {
        [Subject(typeof(DefaultJsonSerializer))]
        public class in_general : WithSubject<DefaultJsonSerializer>
        {
            It should_have_deserialization_settings = () =>
                Subject.DefaultDeserializationSettings.ShouldNotBeNull();
        }

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

        [Subject(typeof(DefaultJsonSerializer))]
        public class when_deserializing_array_with_default_settings : WithSubject<DefaultJsonSerializer>
        {
            Establish context = () =>
                json = SimpleJson.SerializeObject(new[] { "a", "b", "c" });

            Because of = () =>
                deserialized = Subject.DeserializeString<List<string>>(json);

            It should_deserialize_items_when_array = () =>
                deserialized.Count.ShouldEqual(3);

            static string json;

            static List<string> deserialized;
        }

        [Subject(typeof(DefaultJsonSerializer))]
        public class when_deserializing_object_with_default_settings : WithSubject<DefaultJsonSerializer>
        {
            Establish context = () =>
                json = SimpleJson.SerializeObject(new Person { Name = "fred", Age = 30 });

            Because of = () =>
                deserialized = Subject.DeserializeString<Person>(json);

            It should_deserialize_items_when_array = () =>
                deserialized.Name.ShouldEqual("fred");

            static string json;

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
