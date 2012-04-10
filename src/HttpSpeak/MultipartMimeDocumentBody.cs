using System.IO;
using System.Linq;
using System.Text;

namespace HttpSpeak
{
    public class MultipartMimeDocumentBody : ISerializableBody
    {
        private const string Crlf = "\r\n";

        private const string MimeBoundary = "-----------------------------16346778432123";

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

        public void WriteTo(Stream stream)
        {
            foreach (var parameter in resource.Parameters)
            {
                var mimeParameter = string.Format("--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}", MimeBoundary, parameter.Name, parameter.Value, Crlf);
                var encoded = Encoding.UTF8.GetBytes(mimeParameter);

                stream.Write(encoded, 0, encoded.Length);
            }

            foreach (var file in files)
            {
                var fileHeader = string.Format("--{0}{4}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"{4}Content-Type: {3}{4}{4}",
                    MimeBoundary, file.Name, file.FileName, file.ContentType ?? "application/octet-stream", Crlf);

                var encoded = Encoding.UTF8.GetBytes(fileHeader);

                stream.Write(encoded, 0, encoded.Length);

                file.WriteTo(stream);

                var crlf = Encoding.UTF8.GetBytes(Crlf);
                stream.Write(crlf, 0, crlf.Length);
            }

            var footer = Encoding.UTF8.GetBytes(string.Format("--{0}--{1}", MimeBoundary, Crlf));
            stream.Write(footer, 0, footer.Length);
        }
    }
}