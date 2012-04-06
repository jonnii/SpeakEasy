using System.Collections.Generic;
using Resticle.Deserializers;
using Resticle.Serializers;

namespace Resticle
{
    public class Transmission : ITransmission
    {
        public ISerializer DefaultSerializer
        {
            get { return new JsonSerializer(); }
        }

        public IDeserializer GetDeserializeForContentType(string contentType)
        {
            return new JsonDeserializer();
        }

        public IEnumerable<IDeserializer> Deserializers
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public string ContentType
        {
            get { return DefaultSerializer.ContentType; }
        }
    }
}