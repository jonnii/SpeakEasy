using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly IEnumerable<string> products = new[]
        {
             "apples",
             "bananas",
             "cake",
             "dog food"
         };

        [HttpGet]
        public IEnumerable<string> Get(string filter)
        {
            return products.Where(p => p.StartsWith(filter));
        }

        [HttpPost]
        public IActionResult Post(SearchModel searchModel)
        {
            if (searchModel.Username == "unknown-username")
            {
                return StatusCode(422);
            }

            return Created("somewhere", searchModel.Username);
            //return Request.CreateResponse(HttpStatusCode.Created, searchModel.Username);
        }
    }
}