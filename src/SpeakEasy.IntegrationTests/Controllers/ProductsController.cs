using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IEnumerable<Product> products;

        public ProductsController()
        {
            products = new[]
            {
                new Product { Id = 1, Name = "Chocolate Cake", Category = "Foods" },
                new Product { Id = 2, Name = "Ice cream", Category = "Foods" }
            };
        }

        [AcceptVerbs("HEAD")]
        public HttpResponseMessage Head()
        {
            return new HttpResponseMessage();
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage();
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public Product GetProduct(int id)
        {
            return products.Single(p => p.Id == id);
        }

        // public HttpResponseMessage Post([FromBody]Product product)
        // {
        //     if (string.IsNullOrEmpty(product.Name))
        //     {
        //         return Request.CreateResponse(HttpStatusCode.BadRequest, new ValidationError("Name required"));
        //     }

        //     if (string.IsNullOrEmpty(product.Category))
        //     {
        //         return Request.CreateResponse(HttpStatusCode.BadRequest, new ValidationError("Category required"));
        //     }

        //     return new HttpResponseMessage(HttpStatusCode.Created);
        // }

        // public HttpResponseMessage Put(int id, Product product)
        // {
        //     if (string.IsNullOrEmpty(product.Name))
        //     {
        //         return Request.CreateResponse(HttpStatusCode.BadRequest, new ValidationError("Name required"));
        //     }

        //     if (string.IsNullOrEmpty(product.Category))
        //     {
        //         return Request.CreateResponse(HttpStatusCode.BadRequest, new ValidationError("Category required"));
        //     }

        //     var existingProduct = products.FirstOrDefault(p => p.Id == id);

        //     return existingProduct == null
        //         ? new HttpResponseMessage(HttpStatusCode.NotFound)
        //         : new HttpResponseMessage(HttpStatusCode.OK);
        // }

        // [AcceptVerbs("PATCH")]
        // public HttpResponseMessage Patch(int id, Product product)
        // {
        //     if (string.IsNullOrEmpty(product.Name))
        //     {
        //         return Request.CreateResponse(HttpStatusCode.BadRequest, new ValidationError("Name required"));
        //     }

        //     if (string.IsNullOrEmpty(product.Category))
        //     {
        //         return Request.CreateResponse(HttpStatusCode.BadRequest, new ValidationError("Category required"));
        //     }

        //     var existingProduct = products.FirstOrDefault(p => p.Id == id);

        //     return existingProduct == null
        //         ? new HttpResponseMessage(HttpStatusCode.NotFound)
        //         : new HttpResponseMessage(HttpStatusCode.OK);
        // }


        // public HttpResponseMessage Delete(int id)
        // {
        //     var existingProduct = products.FirstOrDefault(p => p.Id == id);

        //     return existingProduct == null
        //         ? new HttpResponseMessage(HttpStatusCode.NotFound)
        //         : new HttpResponseMessage(HttpStatusCode.NoContent);
        // }
    }
}
