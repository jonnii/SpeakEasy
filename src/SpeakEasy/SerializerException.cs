using System;
using System.Runtime.Serialization;

namespace SpeakEasy
{
    [Serializable]
    public class SerializerException : Exception
    {
        public SerializerException()
        {

        }

        public SerializerException(string message)
            : base(message)
        {

        }

        public SerializerException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected SerializerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public string Body { get; set; }
    }
}