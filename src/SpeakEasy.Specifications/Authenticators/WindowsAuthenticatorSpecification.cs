using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Authenticators;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Authenticators
{
    [Subject(typeof(BasicAuthenticator))]
    class WindowsAuthenticatorSpecification : WithFakes
    {
        class when_authenticating
        {
            static WindowsAuthenticator authenticator;

            static IHttpRequest request;

            Establish context = () =>
            {
                request = new GetRequest(new Resource("path"));
                authenticator = new WindowsAuthenticator();
            };

            Because of = () =>
                authenticator.Authenticate(request);

            It should_add_authorization_header = () =>
                request.Credentials.ShouldNotBeNull();
        }
    }
}
