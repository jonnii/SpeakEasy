using System.IO;

namespace SpeakEasy
{
    public interface IFile
    {
        string Name { get; }

        string FileName { get; }

        string ContentType { get; }

        void WriteTo(Stream stream);
    }
}