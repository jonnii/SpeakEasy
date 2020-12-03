using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Authenticators;
using SystemNetClient = System.Net.Http.HttpClient;

namespace SpeakEasy.Specifications.Authenticators
{
    [Subject(typeof(BasicAuthenticator))]
    class BasicAuthenticatorSpecs : WithFakes
    {
        class when_authenticating
        {
            static BasicAuthenticator authenticator;

            static SystemNetClient client;

            Establish context = () =>
            {
                client = new SystemNetClient();
                authenticator = new BasicAuthenticator("username", "password");
            };

            Because of = () =>
                authenticator.Authenticate(client);

            It should_add_authorization_header = () =>
                client.DefaultRequestHeaders.Authorization.ShouldNotBeNull();

            It should_add_basic_authorization = () =>
                client.DefaultRequestHeaders.Authorization.Scheme.ShouldEqual("Basic");
        }
    }
}
