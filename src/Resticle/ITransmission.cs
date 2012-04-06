using System.Collections.Generic;

namespace Resticle
{
    public interface ITransmission
    {
        string ContentType { get; }

        ISerializer DefaultSerializer { get; }

        IEnumerable<string> DeserializableMediaTypes { get; }

        IDeserializer FindDeserializer(string contentType);
    }
}