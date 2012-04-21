using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class SerializableByteArray : ISerializableBody
    {
        public SerializableByteArray(string contentType, byte[] bytes)
        {
            ContentType = contentType;
            Content = bytes;
        }

        public int ContentLength
        {
            get { return Content.Length; }
        }

        public byte[] Content { get; private set; }

        public bool HasContent
        {
            get { return ContentLength > 0; }
        }

        public string ContentType { get; private set; }

        public Task WriteTo(Stream stream)
        {
            return Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, Content, 0, Content.Length, null);
        }
    }
}