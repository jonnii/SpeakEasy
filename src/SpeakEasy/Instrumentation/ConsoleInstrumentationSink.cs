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
            Console.WriteLine("Running request");
            Console.WriteLine(request);
        }

        public void AfterRequest(IHttpRequest request, IHttpResponse response)
        {
            Console.WriteLine("Received Response");
            Console.WriteLine(response);
        }
    }
}
