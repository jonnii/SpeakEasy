using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Resticle.IntegrationTests.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IEnumerable<string> products = new[]
        {
            "apples",
            "bananas",
            "cake", 
            "dog food"
        };

        public IEnumerable<string> Get(string filter)
        {
            return products.Where(p => p.StartsWith(filter));
        }

        public string Post(string username)
        {
            return username;
        }
    }
}