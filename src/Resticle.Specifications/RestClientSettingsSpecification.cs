using Machine.Specifications;
using Resticle.Deserializers;
using Resticle.Serializers;

namespace Resticle.Specifications
{
    public class RestClientSettingsSpecification
    {
        public class default_settings
        {
            Establish context = () =>
                settings = RestClientSettings.Default;

            It should_default_to_json_serializer = () =>
                settings.DefaultSerializer.ShouldBeOfType<JsonSerializer>();

            It should_have_json_deserializer = () =>
                settings.Deserializers.ShouldContain(d => d is JsonDeserializer);

            It should_have_xml_deserializer = () =>
                settings.Deserializers.ShouldContain(d => d is DotNetXmlDeserializer);

            static RestClientSettings settings;
        }
    }
}