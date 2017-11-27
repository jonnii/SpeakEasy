using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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

        [HttpPost]
        public IActionResult Post([FromForm]IList<IFormFile> files)
        {

            return Ok();

            //if (!Request.Content.IsMimeMultipartContent("form-data"))
            //{
            //    return message.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            //}

            //var streamProvider = new MultipartFileStreamProvider(Environment.CurrentDirectory);

            //await Request.Content.ReadAsMultipartAsync(streamProvider);
            //var fileNames = streamProvider.FileData.Select(f => f.Headers.ContentDisposition.Name);

            //return message.CreateResponse(HttpStatusCode.Created, fileNames.ToArray());
        }
    }
}
