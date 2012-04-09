namespace HttpSpeak
{
    public interface IAuthenticator
    {
        void Authenticate(IHttpRequest httpRequest);
    }
}
