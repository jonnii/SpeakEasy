using System.IO;

namespace HttpSpeak
{
    public interface ISerializedBody
    {
        string ContentType { get; }

        int ContentLength { get; }

        bool HasContent { get; }

        void WriteTo(Stream stream);
    }
}