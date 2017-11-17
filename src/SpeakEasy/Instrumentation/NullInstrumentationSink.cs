namespace SpeakEasy.Instrumentation
{
    public class NullInstrumentationSink : IInstrumentationSink
    {
        public static IInstrumentationSink Instance = new NullInstrumentationSink();

        public void BeforeRequest(IHttpRequest request)
        {
        }

        public void AfterRequest(IHttpRequest request, IHttpResponse response, long elapsedMs)
        {
        }
    }
}
