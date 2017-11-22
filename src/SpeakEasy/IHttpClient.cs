using System.Threading;
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
        /// The root resource for this http client, all calls will be relative to this resource
        /// </summary>
        Resource Root { get; }

        /// <summary>
        /// The currently set user agent
        /// </summary>
        IUserAgent UserAgent { get; }

        /// <summary>
        /// Executes an http get request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to get</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Get(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http post request asynchronously
        /// </summary>
        /// <param name="body">An object that represents the body to post</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Post(object body, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http post request with a file upload asynchronously
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Post(IFile file, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http post request with a file uploads asynchronously
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Post(IFile[] files, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http post request without a body asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to post to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Post(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http put request asynchronously
        /// </summary>
        /// <param name="body">An object that represents the body to put</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Put(object body, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http put request without a body asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Put(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http put request with a file upload asynchronously
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Put(IFile file, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http put request with a file uploads asynchronously
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Put(IFile[] files, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http patch request asynchronously
        /// </summary>
        /// <param name="body">An object that represents the body to patch</param>
        /// <param name="relativeUrl">The relative url to put to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Patch(object body, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http patch request without a body asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Patch(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http patch request with a file upload asynchronously
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Patch(IFile file, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http patch request with a file uploads asynchronously
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="relativeUrl">The relative url to patch to</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Patch(IFile[] files, string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http delete request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Delete(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http head request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Head(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes an http options request asynchronously
        /// </summary>
        /// <param name="relativeUrl">The relative url to delete</param>
        /// <param name="segments">An object that contains any segments in the relativeUrl that need to be resolved</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Options(string relativeUrl, object segments = null, CancellationToken cancellationToken = default(CancellationToken));

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
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A chainable http response</returns>
        Task<IHttpResponse> Run<T>(T request, CancellationToken cancellationToken = default(CancellationToken))
            where T : IHttpRequest;
    }
}

