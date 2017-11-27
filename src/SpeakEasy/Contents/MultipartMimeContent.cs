using System.Collections.Generic;
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
        private readonly Resource resource;

        private readonly IFile[] files;

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
                if (!string.IsNullOrEmpty(file.ContentType))
                {
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                }

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"\"{file.FileName}\""
                };

                content.Add(fileContent, file.Name, file.FileName);
            }

            if (resource.HasParameters)
            {
                var formattedParameters = resource
                    .Parameters
                    .Select(t => new KeyValuePair<string, string>(t.Name, t.Value.ToString()));

                content.Add(new FormUrlEncodedContent(formattedParameters));
            }

            httpRequest.Content = content;
        }
    }
}
