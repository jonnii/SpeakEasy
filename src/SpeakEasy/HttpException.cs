using System;

namespace SpeakEasy
{
    public class HttpException : Exception
    {
        public HttpException()
        {
        }

        public HttpException(string message)
            : base(message)
        {
        }

        public HttpException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public HttpException(string message, IHttpResponse response)
            : base(message)
        {
            Response = response;
        }

        public IHttpResponse Response { get; set; }
    }
}
