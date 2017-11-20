using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpeakEasy.Serializers
{
    public class DefaultJsonSerializer : ISerializer
    {
        private readonly JsonSerializer serializer;

        private readonly int serializationBufferSize;

        private readonly Task<bool> okTask = Task.FromResult(true);

        public DefaultJsonSerializer()
            : this(new JsonSerializer())
        {
        }

        public DefaultJsonSerializer(JsonSerializer serializer)
            : this(serializer, 1024)
        {
        }

        public DefaultJsonSerializer(JsonSerializer serializer, int serializationBufferSize)
        {
            this.serializer = serializer;
            this.serializationBufferSize = serializationBufferSize;
        }

        public IEnumerable<string> SupportedMediaTypes => new[]
        {
            "application/json",
            "text/json",
            "text/x-json",
            "text/javascript"
        };

        public string MediaType => SupportedMediaTypes.First();

        public Task SerializeAsync<T>(Stream stream, T body, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var streamWriter = new StreamWriter(stream, new System.Text.UTF8Encoding(false), serializationBufferSize, true))
            {
                using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonTextWriter, body);
                }
            }

            return okTask;
        }

        public T Deserialize<T>(Stream body)
        {
            using (var streamReader = new StreamReader(body))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public object Deserialize(Stream body, Type type)
        {
            using (var streamReader = new StreamReader(body))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize(jsonTextReader, type);
                }
            }
        }
    }
}
