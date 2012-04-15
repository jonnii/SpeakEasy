namespace SpeakEasy.Loggers
{
    public class NullLogger : ILogger
    {
        public static ILogger Instance = new NullLogger();

        public void BeforeRequest(IHttpRequest request)
        {
        }

        public void AfterRequest(IHttpRequest request, IHttpResponse response)
        {
        }
    }
}
