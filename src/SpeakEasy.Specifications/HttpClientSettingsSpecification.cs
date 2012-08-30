using System;
using Machine.Specifications;
using SpeakEasy.Authenticators;
using SpeakEasy.Loggers;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications
{
    public class HttpClientSettingsSpecification
    {
        [Subject(typeof(HttpClientSettings))]
        public class in_general : with_default_settings
        {
            It should_have_null_authenticator = () =>
                settings.Authenticator.ShouldBeOfType<NullAuthenticator>();

            It should_have_null_logger = () =>
                settings.Logger.ShouldBeOfType<NullLogger>();

            It should_have_default_naming_convention = () =>
                settings.NamingConvention.ShouldBeOfType<DefaultNamingConvention>();

            It should_have_default_user_agent = () =>
                settings.UserAgent.Name.ShouldEqual("SpeakEasy");

            It should_be_valid = () =>
                settings.IsValid.ShouldBeTrue();
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

            It should_be_valid = () =>
                settings.IsValid.ShouldBeTrue();
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

        [Subject(typeof(HttpClientSettings))]
        public class without_serializers : with_default_settings
        {
            Because of = () =>
                settings.Serializers.Clear();

            It should_not_be_valid = () =>
                settings.IsValid.ShouldBeFalse();
        }

        [Subject(typeof(HttpClientSettings))]
        public class when_validating_invalid_settings : with_default_settings
        {
            Establish context = () =>
                settings.Serializers.Clear();

            Because of = () =>
                exception = Catch.Exception(() => settings.Validate());

            It should_throw = () =>
                exception.ShouldBeOfType<HttpException>();

            static Exception exception;
        }

        public class with_default_settings
        {
            Establish context = () =>
                settings = new HttpClientSettings();

            protected static HttpClientSettings settings;
        }
    }
}