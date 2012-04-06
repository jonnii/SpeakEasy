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

                settings.Deserializers.Add(new JsonDeserializer());
                settings.Deserializers.Add(new DotNetXmlDeserializer());

                return settings;
            }
        }

        private readonly List<IDeserializer> deserializers = new List<IDeserializer>();

        public ISerializer DefaultSerializer { get; set; }

        public List<IDeserializer> Deserializers
        {
            get { return deserializers; }
        }
    }
}