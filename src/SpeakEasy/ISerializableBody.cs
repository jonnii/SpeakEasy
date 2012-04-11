using System.IO;

namespace SpeakEasy
{
    public interface ISerializableBody
    {
        string ContentType { get; }

        int ContentLength { get; }

        bool HasContent { get; }

        void WriteTo(Stream stream);
    }
}