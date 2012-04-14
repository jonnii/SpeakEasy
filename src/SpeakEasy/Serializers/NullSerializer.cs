using System;
using System.Collections.Generic;
using System.IO;

namespace SpeakEasy.Serializers
{
    public class NullSerializer : ISerializer
    {
        private readonly string contentType;

        public NullSerializer(string contentType)
        {
            this.contentType = contentType;
        }

        public string MediaType
        {
            get { throw new NotImplementedException(); }
        }

        public string Serialize<T>(T t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> SupportedMediaTypes
        {
            get { return new string[0]; }
        }

        public T Deserialize<T>(Stream body)
        {
            var message = string.Format(
                "Could not find a deserializer that supports the content type {0}",
                contentType);

            throw new NotSupportedException(message);
        }

        public T Deserialize<T>(Stream body, DeserializationSettings deserializationSettings)
        {
            return Deserialize<T>(body);
        }
    }
}