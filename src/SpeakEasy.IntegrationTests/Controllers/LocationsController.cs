using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/locations")]
    public class LocationsController : Controller
    {
        [HttpPost]
        public IActionResult Post()
        {
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "titles cannot start with 'bad'";
            return BadRequest();
        }
    }
}
