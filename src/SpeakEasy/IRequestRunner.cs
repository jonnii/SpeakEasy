using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// The request runner is responsible for taking a http request and
    /// running it
    /// </summary>
    public interface IRequestRunner
    {
        //void AddToPipeline()

        /// <summary>
        /// Runs a http request asynchronously
        /// </summary>
        /// <param name="request">The request to run</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A task for an http response</returns>
        Task<IHttpResponse> RunAsync(IHttpRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
