namespace SpeakEasy
{
    /// <summary>
    /// An instrumentation sink 
    /// </summary>
    public interface IInstrumentationSink
    {
        /// <summary>
        /// BeforeRequest is called before every http request is sent
        /// </summary>
        /// <param name="request">The request that is about to be sent</param>
        void BeforeRequest(IHttpRequest request);

        /// <summary>
        /// AfterRequest is called after a request has been sent
        /// </summary>
        /// <param name="request">The http request that was sent</param>
        /// <param name="response">The http response that was returned</param>
        /// <param name="elapsedMs">The time it took for the request to be made</param>
        void AfterRequest(IHttpRequest request, IHttpResponse response, long elapsedMs);
    }
}
