using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class StreamableSerializableBody : ISerializableBody
    {
        private readonly Func<Stream, Task> onStream;

        public StreamableSerializableBody(string contentType, Func<Stream, Task> onStream)
        {
            this.onStream = onStream;
            ContentType = contentType;
        }

        public int ContentLength { get; } = -1;

        public bool HasContent { get; } = true;

        public string ContentType { get; }

        public Task WriteTo(Stream stream)
        {
            return onStream(stream);
        }
    }
}