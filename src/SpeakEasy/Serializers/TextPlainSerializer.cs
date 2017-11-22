using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Serializers
{
    public class TextPlainSerializer : ISerializer
    {
        public string MediaType { get; } = "text/plain";

        public IEnumerable<string> SupportedMediaTypes => new[] { MediaType };

        public Task SerializeAsync<T>(Stream stream, T body, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotSupportedException();
        }

        public T Deserialize<T>(Stream body)
        {
            var deserialized = Deserialize(body, typeof(T));
            return (T)deserialized;
        }

        public object Deserialize(Stream body, Type type)
        {
            using (var reader = new StreamReader(body))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
