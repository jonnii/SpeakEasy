using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakEasy
{
    internal class MultipartMimeDocumentBody : ISerializableBody
    {
        private const string Crlf = "\r\n";

        private const string MimeBoundary = "---------------------------29772313742745";

        private static readonly Encoding DefaultEncoding = new UTF8Encoding(false);

        private readonly Resource resource;

        private readonly IFile[] files;

        public MultipartMimeDocumentBody(Resource resource, IFile[] files)
        {
            this.resource = resource;
            this.files = files;
        }

        public string ContentType
        {
            get { return string.Concat("multipart/form-data; boundary=", MimeBoundary); }
        }

        public int ContentLength
        {
            get { return 0; }
        }

        public bool HasContent
        {
            get { return files.Any(); }
        }

        public string GetFormattedParameter(Parameter parameter)
        {
            return string.Format("--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{2}{3}", MimeBoundary, parameter.Name, parameter.Value, Crlf);
        }

        public async Task WriteTo(Stream stream)
        {
            foreach (var parameter in resource.Parameters)
            {
                await WriteParameter(stream, parameter);
            }

            foreach (var file in files)
            {
                await WriteFile(stream, file);
            }

            await WriteFooter(stream);
        }

        private Task WriteFooter(Stream stream)
        {
            var footer = DefaultEncoding.GetBytes(GetFooter());
            return stream.WriteAsync(footer, 0, footer.Length);
        }

        private Task WriteParameter(Stream stream, Parameter parameter)
        {
            var formattedParameter = GetFormattedParameter(parameter);
            var encoded = DefaultEncoding.GetBytes(formattedParameter);

            return stream.WriteAsync(encoded, 0, encoded.Length);
        }

        private async Task WriteFile(Stream stream, IFile file)
        {
            var fileHeader = GetFileHeader(file);

            var encoded = DefaultEncoding.GetBytes(fileHeader);
            await stream.WriteAsync(encoded, 0, encoded.Length);

            await file.WriteToAsync(stream);

            var crlf = DefaultEncoding.GetBytes(Crlf);
            await stream.WriteAsync(crlf, 0, crlf.Length);
        }

        public string GetFileHeader(IFile file)
        {
            return string.Format(
                "--{0}{4}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"{4}Content-Type: {3}{4}{4}",
                MimeBoundary,
                file.Name,
                file.FileName,
                file.ContentType ?? "application/octet-stream",
                Crlf);
        }

        public string GetFooter()
        {
            return string.Format("--{0}--{1}", MimeBoundary, Crlf);
        }
    }
}