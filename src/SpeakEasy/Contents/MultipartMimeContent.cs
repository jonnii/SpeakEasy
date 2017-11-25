using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    internal class MultipartMimeContent : IContent
    {
        private const string Crlf = "\r\n";

        private const string MimeBoundary = "---------------------------29772313742745";

        private static readonly Encoding DefaultEncoding = new UTF8Encoding(false);

        private readonly Resource resource;

        private readonly IFile[] files;

        public MultipartMimeContent(Resource resource, IFile[] files)
        {
            this.resource = resource;
            this.files = files;
        }

        public string ContentType { get; } = string.Concat("multipart/form-data; boundary=", MimeBoundary);

        public int ContentLength { get; } = 0;

        public bool HasContent => files.Any();

        public string GetFormattedParameter(Parameter parameter)
        {
            return string.Format("--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}", MimeBoundary, parameter.Name, parameter.Value, Crlf);
        }

        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var parameter in resource.Parameters)
            {
                await WriteParameter(stream, parameter, cancellationToken).ConfigureAwait(false);
            }

            foreach (var file in files)
            {
                await WriteFile(stream, file, cancellationToken).ConfigureAwait(false);
            }

            await WriteFooter(stream, cancellationToken).ConfigureAwait(false);
        }

        public async Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var ms = new MemoryStream();
                await file.WriteToAsync(ms);
                ms.Position = 0;

                var byteArrayContent = new StreamContent(ms);
                if (!string.IsNullOrEmpty(file.ContentType))
                {
                    byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                }

                content.Add(byteArrayContent, file.Name, file.FileName);
            }


            //foreach (var parameter in resource.Parameters)
            //{
            //    await WriteParameter(stream, parameter, cancellationToken).ConfigureAwait(false);
            //}

            //content.Add(new MultipartFormDataContent());

            httpRequest.Content = content;

            //httpRequest.Content = new StreamContent(memoryStream);
            //httpRequest.Content.Headers.ContentLength = memoryStream.Length;
            //httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            //return Task.FromResult(0);
        }

        private Task WriteFooter(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            var footer = DefaultEncoding.GetBytes(GetFooter());
            return stream.WriteAsync(footer, 0, footer.Length, cancellationToken);
        }

        private Task WriteParameter(Stream stream, Parameter parameter, CancellationToken cancellationToken = default(CancellationToken))
        {
            var formattedParameter = GetFormattedParameter(parameter);
            var encoded = DefaultEncoding.GetBytes(formattedParameter);

            return stream.WriteAsync(encoded, 0, encoded.Length, cancellationToken);
        }

        private async Task WriteFile(Stream stream, IFile file, CancellationToken cancellationToken = default(CancellationToken))
        {
            var fileHeader = GetFileHeader(file);

            var encoded = DefaultEncoding.GetBytes(fileHeader);
            await stream.WriteAsync(encoded, 0, encoded.Length, cancellationToken).ConfigureAwait(false);

            await file.WriteToAsync(stream).ConfigureAwait(false);

            var crlf = DefaultEncoding.GetBytes(Crlf);
            await stream.WriteAsync(crlf, 0, crlf.Length, cancellationToken).ConfigureAwait(false);
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
            return $"--{MimeBoundary}--{Crlf}";
        }
    }
}
