using System.Collections.Generic;
using System.IO;
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

        public ISerializer DefaultSerializer => serializers.First();

        public string DefaultSerializerContentType => DefaultSerializer.MediaType;

        public IEnumerable<string> DeserializableMediaTypes
        {
            get { return serializers.SelectMany(d => d.SupportedMediaTypes).Distinct(); }
        }

        public void Serialize<T>(Stream stream, T body)
        {
            DefaultSerializer.Serialize(stream, body);
        }

        public ISerializer FindSerializer(string contentType)
        {
            var deserializer = serializers.FirstOrDefault(d => d.SupportedMediaTypes.Any(contentType.StartsWith));

            return deserializer ?? new NullSerializer(contentType);
        }
    }
}
