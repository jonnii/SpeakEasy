using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class FileDownload : IFile
    {
        private readonly Func<Func<Stream, Task>, Task> onStream;

        public FileDownload(string name, string fileName, string contentType, Func<Func<Stream, Task>, Task> onStream)
        {
            this.onStream = onStream;

            Name = name;
            FileName = fileName;
            ContentType = contentType;
        }

        public string Name { get; }

        public string FileName { get; }

        public string ContentType { get; }

        public Task WriteToAsync(Stream stream)
        {
            return onStream(body => body.CopyToAsync(stream));
        }
    }
}
