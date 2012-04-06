using System;

namespace Resticle.Deserializers
{
    public class XmlDeserializer : IDeserializer
    {
        public T Deserialize<T>(string body)
        {
            throw new NotSupportedException();
        }
    }
}