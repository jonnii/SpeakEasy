using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    internal class MultipartFileFormDataContent : IContent
    {
        private readonly Resource resource;

        public MultipartFileFormDataContent(Resource resource)
        {
            this.resource = resource;
        }

        public async Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new MultipartFormDataContent();

            if (resource.HasParameters)
            {
                foreach (var parameter in resource.Parameters)
                {
                    if (parameter.Value is IFile file)
                    {
                        var ms = new MemoryStream();
                        await file.WriteToAsync(ms);

                        var fileContent = new StringContent(Encoding.ASCII.GetString(ms.ToArray()));

                        fileContent.Headers.ContentType = string.IsNullOrWhiteSpace(file.ContentType)
                            ? MediaTypeHeaderValue.Parse("application/octet-stream")
                            : MediaTypeHeaderValue.Parse(file.ContentType);

                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = $"\"{file.Name}\"",
                            FileName = $"\"{file.FileName}\"",
                        };

                        content.Add(fileContent, file.Name, file.FileName);
                    }
                    else
                    {
                        var stringContent = new StringContent(parameter.Value?.ToString() ?? string.Empty);

                        stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = $"\"{parameter.Name}\""
                        };

                        content.Add(stringContent);
                    }
                }
            }

            httpRequest.Content = content;
        }
    }
}
