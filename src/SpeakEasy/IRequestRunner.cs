using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// The request runner is responsible for taking a http request and
    /// running it
    /// </summary>
    public interface IRequestRunner
    {
        /// <summary>
        /// Runs a http request synchronously
        /// </summary>
        /// <param name="request">The request to run</param>
        /// <returns>AN http response</returns>
        IHttpResponse Run(IHttpRequest request);

        /// <summary>
        /// Runs a http request asynchronously
        /// </summary>
        /// <param name="request">The request to run</param>
        /// <returns>A task for an http response</returns>
        Task<IHttpResponse> RunAsync(IHttpRequest request);
    }
}