using Machine.Specifications;
using SpeakEasy.Authenticators;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications
{
    public class HttpClientSettingsSpecification
    {
        [Subject(typeof(HttpClientSettings))]
        public class in_general
        {
            Establish context = () =>
                settings = new HttpClientSettings();

            It should_have_null_authenticator = () =>
                settings.Authenticator.ShouldBeOfType<NullAuthenticator>();

            It should_have_default_user_agent = () =>
                settings.UserAgent.ShouldEqual("SpeakEasy");

            static HttpClientSettings settings;
        }

        [Subject(typeof(HttpClientSettings))]
        public class default_settings_in_general : with_default_settings
        {
            It should_default_to_json_serializer = () =>
                settings.DefaultSerializer.ShouldBeOfType<JsonDotNetSerializer>();

            It should_have_json_deserializer = () =>
                settings.Serializers.ShouldContain(d => d is JsonDotNetSerializer);

            It should_have_xml_deserializer = () =>
                settings.Serializers.ShouldContain(d => d is DotNetXmlSerializer);
        }

        [Subject(typeof(HttpClientSettings))]
        public class when_customizing_serializer : with_default_settings
        {
            Because of = () =>
                settings.Configure<JsonDotNetSerializer>(s =>
                {
                    called = true;
                });

            It should_call_callback = () =>
                called.ShouldEqual(true);

            static bool called;
        }

        public class with_default_settings
        {
            Establish context = () =>
                settings = HttpClientSettings.Default;

            protected static HttpClientSettings settings;
        }
    }
}