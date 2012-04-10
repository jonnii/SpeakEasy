using System.IO;

namespace HttpSpeak
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

        public void WriteTo(Stream stream)
        {
            stream.Write(Content, 0, ContentLength);
        }
    }
}