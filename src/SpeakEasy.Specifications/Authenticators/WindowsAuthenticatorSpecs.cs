using System.Net;
using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Authenticators;

namespace SpeakEasy.Specifications.Authenticators
{
    [Subject(typeof(WindowsAuthenticator))]
    class WindowsAuthenticatorSpecs : WithFakes
    {
        class when_authenticating
        {
            static WindowsAuthenticator authenticator;

            static HttpClientHandler handler;

            Establish context = () =>
            {
                handler = new HttpClientHandler();
                authenticator = new WindowsAuthenticator();
            };

            Because of = () =>
                authenticator.Authenticate(handler);

            It should_add_authorization_header = () =>
                handler.Credentials.ShouldBeTheSameAs(CredentialCache.DefaultCredentials);
        }
    }
}
