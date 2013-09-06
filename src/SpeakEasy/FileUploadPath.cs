using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class FileUploadPath : FileUpload
    {
        private readonly string filePath;

        public FileUploadPath(string name, string filePath)
            : base(name, Path.GetFileName(filePath))
        {
            this.filePath = filePath;
        }

        public override Task WriteToAsync(Stream stream)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    fileStream.CopyToAsync(stream);
                }
            });
        }
    }
}