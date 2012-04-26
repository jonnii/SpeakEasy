using System.IO;
using System.Threading.Tasks;
using SpeakEasy.Extensions;

namespace SpeakEasy
{
    public class FileDownload : IFile
    {
        private readonly Stream body;

        public FileDownload(string name, string fileName, string contentType, Stream body)
        {
            this.body = body;
            Name = name;
            FileName = fileName;
            ContentType = contentType;
        }

        public string Name { get; private set; }

        public string FileName { get; private set; }

        public string ContentType { get; private set; }

        public void WriteTo(Stream stream)
        {
            WriteToAsync(stream).Wait();
        }

        public Task WriteToAsync(Stream stream)
        {
            return body.CopyToAsync(stream);
        }
    }
}