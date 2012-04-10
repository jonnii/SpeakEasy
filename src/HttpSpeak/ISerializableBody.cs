using System.IO;

namespace HttpSpeak
{
    public interface ISerializableBody
    {
        string ContentType { get; }

        int ContentLength { get; }

        bool HasContent { get; }

        void WriteTo(Stream stream);
    }
}