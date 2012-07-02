using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SpeakEasy.IntegrationTests.Controllers
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

        public IEnumerable<string> Get([FromUri]string filter)
        {
            return products.Where(p => p.StartsWith(filter));
        }

        public string Post(SearchModel searchModel)
        {
            return searchModel.Username;
        }

    }
}