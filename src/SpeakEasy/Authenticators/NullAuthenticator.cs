namespace SpeakEasy.Authenticators
{
    public class NullAuthenticator : IAuthenticator
    {
        public void Authenticate(IHttpRequest httpRequest)
        {
        }
    }
}
