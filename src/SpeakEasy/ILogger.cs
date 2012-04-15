namespace SpeakEasy
{
    /// <summary>
    /// A logger is used to log messages from within SpeakEasy
    /// </summary>
    public interface ILogger
    {
        void BeforeRequest(IHttpRequest request);

        void AfterRequest(IHttpRequest request, IHttpResponse response);
    }
}