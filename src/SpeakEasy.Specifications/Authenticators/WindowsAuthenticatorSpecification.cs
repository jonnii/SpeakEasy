using System.Net;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Authenticators;

namespace SpeakEasy.Specifications.Authenticators
{
    public class WindowsAuthenticatorSpecification
    {
        [Subject(typeof(BasicAuthenticator))]
        public class when_authenticating : WithFakes
        {
            Establish context = () =>
            {
                request = new GetRequest(new Resource("path"));
                authenticator = new WindowsAuthenticator();
            };

            Because of = () =>
                authenticator.Authenticate(request);

            It should_add_authorization_header = () =>
                request.Credentials.ShouldNotBeNull();

            static WindowsAuthenticator authenticator;

            static IHttpRequest request;
        }
    }
}