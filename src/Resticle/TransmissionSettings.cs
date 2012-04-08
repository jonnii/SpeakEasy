using System.Collections.Generic;
using System.Linq;
using NLog;
using Resticle.Serializers;

namespace Resticle
{
    public class TransmissionSettings : ITransmissionSettings
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
            Logger.Debug("Finding deserializer for {0}", contentType);

            var deserializer = serializers.FirstOrDefault(d => d.SupportedMediaTypes.Any(contentType.StartsWith));

            return deserializer ?? new NullSerializer(contentType);
        }

        public string Serialize<T>(T body)
        {
            return DefaultSerializer.Serialize(body);
        }
    }
}