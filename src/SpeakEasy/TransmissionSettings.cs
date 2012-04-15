using System.Collections.Generic;
using System.Linq;
using SpeakEasy.Serializers;

namespace SpeakEasy
{
    public class TransmissionSettings : ITransmissionSettings
    {
        private readonly IEnumerable<ISerializer> serializers;

        public TransmissionSettings(IEnumerable<ISerializer> serializers)
        {
            this.serializers = serializers;
        }

        public ISerializer DefaultSerializer
        {
            get { return serializers.First(); }
        }

        public string DefaultSerializerContentType
        {
            get { return DefaultSerializer.MediaType; }
        }

        public IEnumerable<string> DeserializableMediaTypes
        {
            get { return serializers.SelectMany(d => d.SupportedMediaTypes).Distinct(); }
        }

        public ISerializer FindSerializer(string contentType)
        {
            var deserializer = serializers.FirstOrDefault(d => d.SupportedMediaTypes.Any(contentType.StartsWith));

            return deserializer ?? new NullSerializer(contentType);
        }

        public string Serialize<T>(T body)
        {
            return DefaultSerializer.Serialize(body);
        }
    }
}