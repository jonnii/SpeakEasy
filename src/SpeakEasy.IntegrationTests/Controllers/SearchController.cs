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
        public IActionResult Get(string filter)
        {
            var filtered = products
                .Where(p => p.StartsWith(filter))
                .Select(t => new { Name = t })
                .ToArray();

            return Ok(filtered);
        }

        [HttpGet("{category}")]
        public IActionResult Get(string category, string filter)
        {
            var filtered = products
                .Where(p => p.StartsWith(filter))
                .Select(t => new { Name = t, category, filter })
                .ToArray();

            return Ok(filtered);
        }

        [HttpPost]
        public IActionResult Post(SearchModel searchModel)
        {
            if (searchModel.Username == "unknown-username")
            {
                return StatusCode(422);
            }

            return Created("somewhere", new { Name = searchModel.Username });
        }
    }
}
