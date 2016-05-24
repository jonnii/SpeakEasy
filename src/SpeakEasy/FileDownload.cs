using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class FileDownload : IFile
    {
        private readonly Stream body;

        public FileDownload(string name, string fileName, string contentType, Stream body)
        {
            this.body = body;
            Name = name;
            FileName = fileName;
            ContentType = contentType;
        }

        public string Name { get; }

        public string FileName { get; }

        public string ContentType { get; }

        public Task WriteToAsync(Stream stream)
        {
            return body.CopyToAsync(stream);
        }
    }
}
