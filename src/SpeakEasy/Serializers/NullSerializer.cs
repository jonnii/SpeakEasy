using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        public Task SerializeAsync<T>(Stream stream, T body)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> SupportedMediaTypes
        {
            get { return new string[0]; }
        }

        public T Deserialize<T>(Stream body)
        {
            throw BuildNotSupportedException();
        }

        public object Deserialize(Stream body, Type type)
        {
            throw BuildNotSupportedException();
        }

        public object Deserialize(Stream body, DeserializationSettings deserializationSettings, Type type)
        {
            return Deserialize(body, type);
        }

        public T Deserialize<T>(Stream body, DeserializationSettings deserializationSettings)
        {
            return Deserialize<T>(body);
        }

        private NotSupportedException BuildNotSupportedException()
        {
            var message = string.Format(
                "Could not find a deserializer that supports the content type {0}",
                contentType);

            return new NotSupportedException(message);
        }
    }
}