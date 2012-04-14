using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpeakEasy.Serializers
{
    public abstract class Serializer : ISerializer
    {
        protected Serializer()
        {
            DefaultDeserializationSettings = new DeserializationSettings();
        }

        public virtual string MediaType
        {
            get { return SupportedMediaTypes.First(); }
        }

        public abstract IEnumerable<string> SupportedMediaTypes { get; }

        public DeserializationSettings DefaultDeserializationSettings { get; set; }

        public abstract string Serialize<T>(T t);

        public virtual T Deserialize<T>(Stream body)
        {
            return Deserialize<T>(body, DefaultDeserializationSettings);
        }

        public abstract T Deserialize<T>(Stream body, DeserializationSettings deserializationSettings);
    }
}