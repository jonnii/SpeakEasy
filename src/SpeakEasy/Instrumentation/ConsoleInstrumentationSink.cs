using System;

namespace SpeakEasy.Instrumentation
{
    /// <summary>
    /// Console instrumentation sink outputs all instrumentation to the console
    /// </summary>
    public class ConsoleInstrumentationSink : IInstrumentationSink
    {
        public void BeforeRequest(IHttpRequest request)
        {
            Console.WriteLine("Running request: {0}", request);
        }

        public void AfterRequest(IHttpRequest request, IHttpResponse response, long elapsedMs)
        {
            Console.WriteLine("Received Response: {0} in {1}ms", response, elapsedMs);
        }
    }
}
