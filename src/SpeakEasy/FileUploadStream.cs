using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class FileUploadStream : FileUpload
    {
        private readonly Stream contents;

        public FileUploadStream(string name, string fileName, Stream contents)
            : base(name, fileName)
        {
            this.contents = contents;
        }

        public override Task WriteToAsync(Stream stream)
        {
            return contents.CopyToAsync(stream);
        }
    }
}
