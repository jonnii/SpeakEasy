namespace HttpSpeak.Authenticators
{
    public class NullAuthenticator : IAuthenticator
    {
        public void Authenticate(IHttpRequest httpRequest)
        {

        }
    }
}