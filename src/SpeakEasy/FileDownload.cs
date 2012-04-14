using System.IO;

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
            body.CopyTo(stream);
        }
    }
}