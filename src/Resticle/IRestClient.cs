namespace Resticle
{
    /// <summary>
    /// The IRestClient is your entry point into a restful API. The methods map to HttpMethods methods on the server (GET/PUT/POST/PATCH/DELETE etc...) and 
    /// return a chainable rest response.
    /// </summary>
    public interface IRestClient
    {
        IRestResponse Get(string url, object segments = null);

        IRestResponse Post(object body, string url, object segments = null);
    }
}

