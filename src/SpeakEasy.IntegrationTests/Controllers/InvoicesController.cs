using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/invoices")]
    public class InvoicesController : Controller
    {
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return File(Encoding.UTF8.GetBytes("file contents"), "application/octet-stream", "foo.txt");
        }

        //public async Task<HttpResponseMessage> Post(HttpRequestMessage message)
        //{
        //    if (!Request.Content.IsMimeMultipartContent("form-data"))
        //    {
        //        return message.CreateResponse(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var streamProvider = new MultipartFileStreamProvider(Environment.CurrentDirectory);

        //    await Request.Content.ReadAsMultipartAsync(streamProvider);
        //    var fileNames = streamProvider.FileData.Select(f => f.Headers.ContentDisposition.Name);

        //    return message.CreateResponse(HttpStatusCode.Created, fileNames.ToArray());
        //}
    }
}
