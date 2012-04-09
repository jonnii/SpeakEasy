namespace HttpSpeak
{
    /// <summary>
    /// The request runner is responsible for taking a http request and
    /// running it
    /// </summary>
    public interface IRequestRunner
    {
        /// <summary>
        /// Runs a http request
        /// </summary>
        /// <param name="request">The request to run</param>
        /// <returns>A new response</returns>
        IHttpResponse Run(IHttpRequest request);
    }
}