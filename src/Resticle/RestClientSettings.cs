using System.Collections.Generic;
using Resticle.Deserializers;
using Resticle.Serializers;

namespace Resticle
{
    public class RestClientSettings
    {
        public static RestClientSettings Default
        {
            get
            {
                var settings = new RestClientSettings
                {
                    DefaultSerializer = new JsonSerializer()
                };

                settings.AddDeserializer(new JsonDeserializer());
                settings.AddDeserializer(new DotNetXmlDeserializer());

                return settings;
            }
        }

        private readonly List<IDeserializer> deserializers = new List<IDeserializer>();

        public ISerializer DefaultSerializer { get; set; }

        public IEnumerable<IDeserializer> Deserializers
        {
            get { return deserializers; }
        }

        private void AddDeserializer(IDeserializer deserializer)
        {
            deserializers.Add(deserializer);
        }
    }
}