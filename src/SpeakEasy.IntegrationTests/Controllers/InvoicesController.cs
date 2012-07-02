using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public HttpResponseMessage Post(HttpRequestMessage message)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return message.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var streamProvider = new MultipartFileStreamProvider(Environment.CurrentDirectory);

            var bodyParts = Request.Content.ReadAsMultipartAsync(streamProvider);
            bodyParts.Wait();

            var fileNames = streamProvider.BodyPartFileNames;

            return message.CreateResponse(HttpStatusCode.Created, fileNames);
        }
    }
}