namespace SpeakEasy
{
    public interface IAuthenticator
    {
        void Authenticate(IHttpRequest httpRequest);
    }
}
