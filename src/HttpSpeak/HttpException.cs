using System;
using System.Runtime.Serialization;

namespace HttpSpeak
{
    [Serializable]
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

        protected HttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
