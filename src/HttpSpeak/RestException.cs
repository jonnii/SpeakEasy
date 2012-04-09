using System;
using System.Runtime.Serialization;

namespace HttpSpeak
{
    [Serializable]
    public class RestException : Exception
    {
        public RestException()
        {
        }

        public RestException(string message)
            : base(message)
        {
        }

        public RestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
