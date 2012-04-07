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
        Resource Root { get; }

        /// <summary>
        /// Executes an http get request
        /// </summary>
        /// <param name="resource">The resource to get</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Get(Resource resource);

        /// <summary>
        /// Executes an http get request
        /// </summary>
        /// <param name="relativeUrl">The relative url to get</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Get(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request
        /// </summary>
        /// <param name="body">An object that represents the body to post</param>
        /// <param name="resource">The resource to post</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(object body, Resource resource);

        /// <summary>
        /// Executes an http post request
        /// </summary>
        /// <param name="body">An object that represents the body to post</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request with a file upload
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(FileUpload file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request with a file uploads
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(FileUpload[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request without a body
        /// </summary>
        /// <param name="resource">The resource to post</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(Resource resource);

        /// <summary>
        /// Executes an http post request without a body
        /// </summary>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Post(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request
        /// </summary>
        /// <param name="body">An object that represents the body to put</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Put(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request without a body
        /// </summary>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Put(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request with a file upload
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Put(FileUpload file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request with a file uploads
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Put(FileUpload[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http delete request
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Delete(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http head request
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable rest response</returns>
        IRestResponse Head(string relativeUrl, object segments = null);
    }
}

