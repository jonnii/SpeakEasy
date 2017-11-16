using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// we don't need this class
namespace SpeakEasy.Serializers
{
    public abstract class Serializer : ISerializer
    {
        public virtual string MediaType => SupportedMediaTypes.First();

        public abstract IEnumerable<string> SupportedMediaTypes { get; }

        public abstract Task SerializeAsync<T>(Stream stream, T body, CancellationToken cancellationToken = default(CancellationToken));

        public abstract object Deserialize(Stream body, Type type);

        public abstract T Deserialize<T>(Stream body);
    }
}
