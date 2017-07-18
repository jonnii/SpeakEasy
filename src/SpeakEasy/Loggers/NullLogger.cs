namespace SpeakEasy.Loggers
{
    public class NullLogger : ISpeakEasyLogger
    {
        public static ISpeakEasyLogger Instance = new NullLogger();

        public void BeforeRequest(IHttpRequest request)
        {
        }

        public void AfterRequest(IHttpRequest request, IHttpResponse response)
        {
        }
    }
}
