namespace Resticle
{
    public interface IAuthenticator
    {
        void Authenticate(IRestRequest restRequest);
    }
}
