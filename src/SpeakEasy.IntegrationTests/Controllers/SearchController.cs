// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Web.Http;

// namespace SpeakEasy.IntegrationTests.Controllers
// {
//     public class SearchController : ApiController
//     {
//         private readonly IEnumerable<string> products = new[]
//         {
//             "apples",
//             "bananas",
//             "cake", 
//             "dog food"
//         };

//         public IEnumerable<string> Get([FromUri]string filter)
//         {
//             return products.Where(p => p.StartsWith(filter));
//         }

//         public HttpResponseMessage Post(SearchModel searchModel)
//         {
//             if (searchModel.Username == "unknown-username")
//             {
//                 throw new HttpResponseException((HttpStatusCode)422);
//             }

//             return Request.CreateResponse(HttpStatusCode.Created, searchModel.Username);
//         }
//     }
// }