using Machine.Specifications;
using Newtonsoft.Json;
using Resticle.Serializers;

namespace Resticle.Specifications.Serializers
{
    public class JsonDotNetSerializerSpecification
    {
        [Subject(typeof(JsonDotNetSerializer))]
        public class when_configuring_settings
        {
            Establish context = () =>
                serializer = new JsonDotNetSerializer();

            Because of = () =>
                serializer.ConfigureSettings(s => s.Formatting = Formatting.Indented);

            It should_set_formatting = () =>
                serializer.ConfigureSettings(s => s.Formatting.ShouldEqual(Formatting.Indented));

            static JsonDotNetSerializer serializer;
        }
    }
}
