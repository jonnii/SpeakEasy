using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public abstract class FileUpload : IFile
    {
        public static FileUpload FromBytes(string name, string fileName, byte[] contents)
        {
            return new FileUploadByteArray(name, fileName, contents);
        }

        public static FileUpload FromStream(string name, string fileName, Stream contents)
        {
            return new FileUploadStream(name, fileName, contents);
        }

        public static FileUpload FromPath(string name, string filePath)
        {
            return new FileUploadPath(name, filePath);
        }

        protected FileUpload(string name, string fileName)
        {
            Name = name;
            FileName = fileName;
        }

        public string Name { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public void WriteTo(Stream stream)
        {
            WriteToAsync(stream).Wait();
        }

        public abstract Task WriteToAsync(Stream stream);
    }
}
