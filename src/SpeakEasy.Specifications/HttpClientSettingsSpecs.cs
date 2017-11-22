using System;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;
using SpeakEasy.ArrayFormatters;
using SpeakEasy.Authenticators;
using SpeakEasy.Middleware;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpClientSettings))]
    class HttpClientSettingsSpecs
    {
        static HttpClientSettings settings;

        Establish context = () =>
            settings = new HttpClientSettings();

        class in_general
        {
            It should_have_null_authenticator = () =>
                settings.Authenticator.ShouldBeOfExactType<NullAuthenticator>();

            It should_have_default_naming_convention = () =>
                settings.NamingConvention.ShouldBeOfExactType<DefaultNamingConvention>();

            It should_have_default_user_agent = () =>
                settings.Middleware.Has<UserAgentMiddleware>().ShouldBeTrue();

            It should_have_default_array_formatter = () =>
                settings.ArrayFormatter.ShouldBeOfExactType<MultipleValuesArrayFormatter>();

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

        class without_array_formatter
        {
            Because of = () =>
                settings.ArrayFormatter = null;

            It should_not_be_valid = () =>
                settings.IsValid.ShouldBeFalse();
        }

        class when_customizing_serializer
        {
            static bool called;

            Because of = () =>
                settings.Configure<DefaultJsonSerializer>(s =>
                {
                    called = true;
                });

            It should_call_callback = () =>
                called.ShouldEqual(true);
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
            static Exception exception;

            Establish context = () =>
                settings.Serializers.Clear();

            Because of = () =>
                exception = Catch.Exception(() => settings.Validate());

            It should_throw = () =>
                exception.ShouldBeOfExactType<InvalidOperationException>();
        }

        class when_appending_middleware
        {
            Because of = () =>
                settings.Middleware.Append(new TestMiddleware());

            It should_have_middleware = () =>
                settings.Middleware.Has<TestMiddleware>().ShouldBeTrue();

            It should_add_to_end_of_middleware_chain = () =>
                settings.Middleware.AtPosition(settings.Middleware.Count - 1).ShouldBeOfExactType<TestMiddleware>();
        }

        class when_prepending_middleware
        {
            Because of = () =>
                settings.Middleware.Prepend(new TestMiddleware());

            It should_have_middleware = () =>
                settings.Middleware.Has<TestMiddleware>().ShouldBeTrue();

            It should_add_to_start_of_middleware_chain = () =>
                settings.Middleware.AtPosition(0).ShouldBeOfExactType<TestMiddleware>();
        }

        class when_replacing_middleware_with_same_type
        {
            static UserAgentMiddleware replacement;

            Establish context = () =>
            {
                settings.Middleware.Prepend(new TestMiddleware());
                settings.Middleware.Append(new TestMiddleware());

                replacement = new UserAgentMiddleware();
            };

            Because of = () =>
                settings.Middleware.Replace(replacement);

            It should_replace_middleware_in_place = () =>
                settings.Middleware.AtPosition(1).ShouldBeOfExactType<UserAgentMiddleware>();

            It should_have_correct_middleware_count = () =>
                settings.Middleware.Count.ShouldEqual(3);

            It should_replace_instance = () =>
                settings.Middleware.AtPosition(1).ShouldBeTheSameAs(replacement);
        }

        class TestMiddleware : IHttpMiddleware
        {
            public IHttpMiddleware Next { get; set; }

            public Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
