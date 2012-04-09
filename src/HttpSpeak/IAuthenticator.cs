namespace HttpSpeak
{
    public interface IAuthenticator
    {
        void Authenticate(IRestRequest restRequest);
    }
}
