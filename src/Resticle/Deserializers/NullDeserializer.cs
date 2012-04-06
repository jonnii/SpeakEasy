using System;
using System.Collections.Generic;

namespace Resticle.Deserializers
{
    public class NullDeserializer : IDeserializer
    {
        private readonly string contentType;

        public NullDeserializer(string contentType)
        {
            this.contentType = contentType;
        }

        public IEnumerable<string> SupportedMediaTypes
        {
            get { return new string[0]; }
        }

        public T Deserialize<T>(string body)
        {
            var message = string.Format(
                "Could not find a deserializer that supports the content type {0}",
                contentType);

            throw new NotSupportedException(message);
        }
    }
}