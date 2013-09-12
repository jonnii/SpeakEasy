using System;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// The IHttpClient is your entry point into an API that speaks http. The methods map to HttpMethods 
    /// methods on the server (GET/PUT/POST/PATCH/DELETE etc...) and return a chainable http response.
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// An event that is raised before each request is run
        /// </summary>
        event EventHandler<BeforeRequestEventArgs> BeforeRequest;

        /// <summary>
        /// An event that is raised after each request is s
        /// </summary>
        event EventHandler<AfterRequestEventArgs> AfterRequest;

        /// <summary>
        /// The root resource for this http client, all calls will be relative to this resource
        /// </summary>
        Resource Root { get; }

        /// <summary>
        /// The currently set user agent
        /// </summary>
        IUserAgent UserAgent { get; }

        /// <summary>
        /// Executes an http get request
        /// </summary>
        /// <param name="relativeUrl">The relative url to get</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Get(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request
        /// </summary>
        /// <param name="body">An object that represents the body to post</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Post(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request with a file upload
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Post(IFile file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request with a file uploads
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Post(IFile[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request without a body
        /// </summary>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Post(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request
        /// </summary>
        /// <param name="body">An object that represents the body to put</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Put(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request without a body
        /// </summary>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Put(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request with a file upload
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Put(IFile file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request with a file uploads
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Put(IFile[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request
        /// </summary>
        /// <param name="body">An object that represents the body to patch</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Patch(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request without a body
        /// </summary>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Patch(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request with a file upload
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Patch(IFile file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request with a file uploads
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Patch(IFile[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http delete request
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Delete(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http head request
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Head(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http options request
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        IHttpResponse Options(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http get request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to get</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> GetAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request asynchronously
        /// </summary>
        /// <param name="body">An object that represents the body to post</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PostAsync(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request with a file upload asynchronously
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PostAsync(IFile file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request with a file uploads asynchronously
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PostAsync(IFile[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http post request without a body asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PostAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request asynchronously
        /// </summary>
        /// <param name="body">An object that represents the body to put</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PutAsync(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request without a body asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PutAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request with a file upload asynchronously
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PutAsync(IFile file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http put request with a file uploads asynchronously
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PutAsync(IFile[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request asynchronously
        /// </summary>
        /// <param name="body">An object that represents the body to patch</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PatchAsync(object body, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request without a body asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PatchAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request with a file upload asynchronously
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PatchAsync(IFile file, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http patch request with a file uploads asynchronously
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> PatchAsync(IFile[] files, string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http delete request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> DeleteAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http head request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> HeadAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Executes an http options request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> OptionsAsync(string relativeUrl, object segments = null);

        /// <summary>
        /// Builds a relative resource
        /// </summary>
        /// <param name="relativeUrl">The relative url</param>
        /// <param name="segments">Any segments that need to be merged into the url</param>
        /// <param name="shouldMergeProperties">Whether or not to merge properties into the url</param>
        /// <returns>A relative resource that can be used to run requests</returns>
        Resource BuildRelativeResource(string relativeUrl, object segments, bool shouldMergeProperties = true);

        /// <summary>
        /// Runs an http request
        /// </summary>
        /// <typeparam name="T">The type of request to run</typeparam>
        /// <param name="request">the request</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> RunAsync<T>(T request)
            where T : IHttpRequest;
    }
}

