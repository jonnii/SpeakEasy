using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/middleware")]
    public class MiddlewareController : Controller
    {
        [HttpGet("echo-header")]
        public IActionResult Get([FromQuery]string headerName)
        {
            Request.Headers.TryGetValue(headerName, out var vals);

            var header = vals.First();

            return new ContentResult
            {
                Content = header,
                ContentType = "text/plain",
                StatusCode = 200
            };
        }

        [HttpGet("echo-user-agent")]
        public IActionResult Get()
        {
            var userAgent = Request.Headers["User-Agent"].ToString();

            return new ContentResult
            {
                Content = userAgent,
                ContentType = "text/plain",
                StatusCode = 200
            };
        }
    }
}
