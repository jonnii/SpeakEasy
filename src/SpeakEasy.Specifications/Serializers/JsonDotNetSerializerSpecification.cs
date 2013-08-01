//using System.Collections.Generic;
//using Machine.Fakes;
//using Machine.Specifications;
//using Newtonsoft.Json;
//using SpeakEasy.Serializers;

//namespace SpeakEasy.Specifications.Serializers
//{
//    public class JsonDotNetSerializerSpecification
//    {
//        [Subject(typeof(JsonDotNetSerializer))]
//        public class in_general : WithSubject<JsonDotNetSerializer>
//        {
//            It should_have_deserialization_settings = () =>
//                Subject.DefaultDeserializationSettings.ShouldNotBeNull();
//        }

//        [Subject(typeof(JsonDotNetSerializer))]
//        public class when_configuring_settings : WithSubject<JsonDotNetSerializer>
//        {
//            Because of = () =>
//                Subject.ConfigureSettings(s => s.Formatting = Formatting.Indented);

//            It should_set_formatting = () =>
//                Subject.ConfigureSettings(s => s.Formatting.ShouldEqual(Formatting.Indented));
//        }

//        [Subject(typeof(JsonDotNetSerializer))]
//        public class when_deserializing_array_with_default_settings : WithSubject<JsonDotNetSerializer>
//        {
//            Establish context = () =>
//                json = JsonConvert.SerializeObject(new[] { "a", "b", "c" });

//            Because of = () =>
//                deserialized = Subject.DeserializeString<List<string>>(json);

//            It should_deserialize_items_when_array = () =>
//                deserialized.Count.ShouldEqual(3);

//            static string json;

//            static List<string> deserialized;
//        }

//        [Subject(typeof(JsonDotNetSerializer))]
//        public class when_deserializing_object_with_default_settings : WithSubject<JsonDotNetSerializer>
//        {
//            Establish context = () =>
//                json = JsonConvert.SerializeObject(new { name = "fred", age = 30 });

//            Because of = () =>
//                deserialized = Subject.DeserializeString<object>(json);

//            It should_deserialize_items_when_array = () =>
//                ((string)deserialized.name).ShouldEqual("fred");

//            static string json;

//            static dynamic deserialized;
//        }

//        [Subject(typeof(JsonDotNetSerializer))]
//        public class when_deserializing_collection_with_settings_with_root_path : WithSubject<JsonDotNetSerializer>
//        {
//            Establish context = () =>
//                json = JsonConvert.SerializeObject(new { root = new { name = "fred", age = 30 } });

//            Because of = () =>
//                deserialized = Subject.DeserializeString<object>(json, new DeserializationSettings { RootElementPath = "root" });

//            It should_deserialize_items_when_array = () =>
//                ((string)deserialized.name).ShouldEqual("fred");

//            static string json;

//            static dynamic deserialized;
//        }

//        [Subject(typeof(JsonDotNetSerializer))]
//        public class when_deserializing_collection_with_settings_with_skip_root_element : WithSubject<JsonDotNetSerializer>
//        {
//            Establish context = () =>
//                json = JsonConvert.SerializeObject(new { root = new { name = "fred", age = 30 } });

//            Because of = () =>
//                deserialized = Subject.DeserializeString<object>(json, new DeserializationSettings { SkipRootElement = true });

//            It should_skip_root_element = () =>
//                ((string)deserialized.name).ShouldEqual("fred");

//            static string json;

//            static dynamic deserialized;
//        }
//    }
//}
