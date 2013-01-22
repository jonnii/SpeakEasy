using System;

namespace SpeakEasy.Loggers
{
    /// <summary>
    /// Console logger outputs all logging information to the console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void BeforeRequest(IHttpRequest request)
        {
            //Console.WriteLine("Running request");
            //Console.WriteLine(request);
        }

        public void AfterRequest(IHttpRequest request, IHttpResponse response)
        {
            //Console.WriteLine("Received Response");
            //Console.WriteLine(response);
        }
    }
}