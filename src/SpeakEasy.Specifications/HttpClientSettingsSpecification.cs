using System;
using Machine.Specifications;
using SpeakEasy.Authenticators;
using SpeakEasy.Loggers;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpClientSettings))]
    class HttpClientSettingsSpecification
    {
        static HttpClientSettings settings;

        Establish context = () =>
            settings = new HttpClientSettings();

        class in_general
        {
            It should_have_null_authenticator = () =>
                settings.Authenticator.ShouldBeOfExactType<NullAuthenticator>();

            It should_have_null_logger = () =>
                settings.Logger.ShouldBeOfExactType<NullLogger>();

            It should_have_default_naming_convention = () =>
                settings.NamingConvention.ShouldBeOfExactType<DefaultNamingConvention>();

            It should_have_default_user_agent = () =>
                settings.UserAgent.Name.ShouldEqual("SpeakEasy");

            It should_have_default_cookie_container = () =>
                settings.CookieStrategy.ShouldBeOfExactType<TransientCookieStrategy>();

            It should_have_default_array_formatter = () =>
                settings.ArrayFormatter.ShouldBeOfExactType<MultipleValuesArrayFormatter>();

            It should_have_default_stream_manager = () =>
                settings.StreamManager.ShouldBeOfExactType<DefaultStreamManager>();

            It should_be_valid = () =>
                settings.IsValid.ShouldBeTrue();
        }

        class default_settings_in_general
        {
            It should_default_to_json_serializer = () =>
                settings.DefaultSerializer.ShouldBeOfExactType<DefaultJsonSerializer>();

            It should_have_json_deserializer = () =>
                settings.Serializers.ShouldContain(d => d is DefaultJsonSerializer);

            It should_be_valid = () =>
                settings.IsValid.ShouldBeTrue();
        }

        class without_cookie_strategy
        {
            Because of = () =>
                settings.CookieStrategy = null;

            It should_not_be_valid = () =>
                settings.IsValid.ShouldBeFalse();
        }

        class without_array_formatter
        {
            Because of = () =>
                settings.ArrayFormatter = null;

            It should_not_be_valid = () =>
                settings.IsValid.ShouldBeFalse();
        }

        class when_customizing_serializer
        {
            Because of = () =>
                settings.Configure<DefaultJsonSerializer>(s =>
                {
                    called = true;
                });

            It should_call_callback = () =>
                called.ShouldEqual(true);

            static bool called;
        }

        class without_serializers
        {
            Because of = () =>
                settings.Serializers.Clear();

            It should_not_be_valid = () =>
                settings.IsValid.ShouldBeFalse();
        }

        class when_validating_invalid_settings
        {
            Establish context = () =>
                settings.Serializers.Clear();

            Because of = () =>
                exception = Catch.Exception(() => settings.Validate());

            It should_throw = () =>
                exception.ShouldBeOfExactType<ConfigurationException>();

            static Exception exception;
        }
    }
}
