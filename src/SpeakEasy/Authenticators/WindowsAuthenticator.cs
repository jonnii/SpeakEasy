using System.Net;

namespace SpeakEasy.Authenticators
{
    public class WindowsAuthenticator : IAuthenticator
    {
        public void Authenticate(IHttpRequest httpRequest)
        {
            httpRequest.Credentials = CredentialCache.DefaultCredentials;
        }
    }
}