using System;

namespace Resticle
{
    /// <summary>
    /// The IRestClient is your entry point into a restful API. The methods map to HttpMethods methods on the server (GET/PUT/POST/PATCH/DELETE etc...) and 
    /// return a chainable rest response.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// The root resource for this rest client, all calls will be relative to this resource
        /// </summary>
        Resource Root { get; set; }

        /// <summary>
        /// The default serializer to use when formatting the body of rest requests
        /// </summary>
        Type DefaultSerializer { get; set; }

        /// <summary>
        /// Executes an http get request
        /// </summary>
        /// <param name="url">The url to get</param>
        /// <param name="segments">An object that contains any segments in the url that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Get(string url, object segments = null);

        /// <summary>
        /// Executes an http post request
        /// </summary>
        /// <param name="body">An object that represents the body to post</param>
        /// <param name="url">The url to post to</param>
        /// <param name="segments">An object that contains any segments in the url that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(object body, string url, object segments = null);

        /// <summary>
        /// Executes an http put request
        /// </summary>
        /// <param name="body">An object that represents the body to put</param>
        /// <param name="url">The url to put to</param>
        /// <param name="segments">An object that contains any segments in the url that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Put(object body, string url, object segments = null);

        /// <summary>
        /// Executes an http delete request
        /// </summary>
        /// <param name="url">The url to delete</param>
        /// <param name="segments">An object that contains any segments in the url that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Delete(string url, object segments = null);
    }
}

