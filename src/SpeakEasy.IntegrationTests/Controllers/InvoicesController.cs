using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeakEasy.IntegrationTests.Controllers
{
    public class InvoicesController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent("file contents")
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "foo.txt",
                Name = "name",
            };

            return response;
        }

        public async Task<HttpResponseMessage> Post(HttpRequestMessage message)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return message.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var streamProvider = new MultipartFileStreamProvider(Environment.CurrentDirectory);

            await Request.Content.ReadAsMultipartAsync(streamProvider);
            var fileNames = streamProvider.FileData.Select(f => f.Headers.ContentDisposition.Name);

            return message.CreateResponse(HttpStatusCode.Created, fileNames.ToArray());
        }
    }
}