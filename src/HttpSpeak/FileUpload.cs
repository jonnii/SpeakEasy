using System.IO;

namespace HttpSpeak
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
            stream.Write(contents, 0, contents.Length);
        }
    }
}
