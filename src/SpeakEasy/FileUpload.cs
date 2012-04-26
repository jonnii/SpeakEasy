using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class FileUpload : IFile
    {
        private readonly byte[] contents;

        public FileUpload(string name, string fileName, byte[] contents)
        {
            Name = name;
            FileName = fileName;

            this.contents = contents;
        }

        public string Name { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public void WriteTo(Stream stream)
        {
            WriteToAsync(stream).Wait();
        }

        public Task WriteToAsync(Stream stream)
        {
            return Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, contents, 0, contents.Length, null);
        }
    }
}
