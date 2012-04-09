namespace Resticle.Authenticators
{
    public class NullAuthenticator : IAuthenticator
    {
        public void Authenticate(IRestRequest restRequest)
        {

        }
    }
}