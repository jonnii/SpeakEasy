using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    internal class MultipartMimeContent : IContent
    {
        private readonly Resource resource;

        private readonly IFile[] files;

        private const string MimeBoundary = "---------------------------29772313742745";

        private const string Crlf = "\r\n";

        public MultipartMimeContent(Resource resource, IFile[] files)
        {
            this.resource = resource;
            this.files = files;
        }

        public bool HasContent => files.Any();

        public async Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var ms = new MemoryStream();
                await file.WriteToAsync(ms);
                ms.Position = 0;

                var fileContent = new StreamContent(ms);

                fileContent.Headers.ContentType = string.IsNullOrWhiteSpace(file.ContentType)
                    ? MediaTypeHeaderValue.Parse("application/octet-stream")
                    : MediaTypeHeaderValue.Parse(file.ContentType);

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"\"{file.FileName}\""
                };

                content.Add(fileContent, file.Name, file.FileName);
            }

            if (resource.HasParameters)
            {
                foreach (var parameter in resource.Parameters)
                {
                    var stringContent = new StringContent(parameter.Value?.ToString() ?? string.Empty);

                    stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = parameter.Name
                    };

                    content.Add(stringContent);
                }
            }

            httpRequest.Content = content;
        }
    }
}
