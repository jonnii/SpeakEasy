using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public interface IFile
    {
        string Name { get; }

        string FileName { get; }

        string ContentType { get; }

        void WriteTo(Stream stream);

        Task WriteToAsync(Stream stream);
    }
}