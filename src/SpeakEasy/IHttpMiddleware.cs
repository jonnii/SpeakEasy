using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// An http middleware can be used to intercept either the http request or http response
    /// and to apply additional processing
    /// </summary>
    public interface IHttpMiddleware
    {
        /// <summary>
        /// The next middleware in the middleware chain
        /// </summary>
        IHttpMiddleware Next { get; set; }

        /// <summary>
        /// Invokes the middleware
        /// </summary>
        /// <param name="request">The http request</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task that completes with an IHttpResponse</returns>
        Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken);
    }
}
