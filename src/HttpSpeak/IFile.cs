using System.IO;

namespace HttpSpeak
{
    public interface IFile
    {
        string Name { get; }

        string FileName { get; }

        string ContentType { get; }

        void WriteTo(Stream stream);
    }
}