using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpeakEasy.Serializers;

namespace SpeakEasy
{
    internal class TransmissionSettings : ITransmissionSettings
    {
        private readonly IEnumerable<ISerializer> serializers;

        public TransmissionSettings(IEnumerable<ISerializer> serializers)
        {
            this.serializers = serializers;
        }

        public ISerializer DefaultSerializer => serializers.First();

        public string DefaultSerializerContentType => DefaultSerializer.MediaType;

        public IEnumerable<string> DeserializableMediaTypes
        {
            get { return serializers.SelectMany(d => d.SupportedMediaTypes).Distinct(); }
        }

        public Task SerializeAsync<T>(Stream stream, T body, CancellationToken cancellationToken = default(CancellationToken))
        {
            return DefaultSerializer.SerializeAsync(stream, body, cancellationToken);
        }

        public ISerializer FindSerializer(string contentType)
        {
            var deserializer = serializers.FirstOrDefault(d => d.SupportedMediaTypes.Any(contentType.StartsWith));

            return deserializer ?? new NullSerializer(contentType);
        }
    }
}
