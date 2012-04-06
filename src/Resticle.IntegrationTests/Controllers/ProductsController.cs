using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Resticle.IntegrationTests.Controllers
{
    public class ProductsController : ApiController
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

        public IEnumerable<Product> Get()
        {
            return products;
        }

        public Product Get(int id)
        {
            return products.Single(p => p.Id == id);
        }

        public HttpResponseMessage Post(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return new HttpResponseMessage<ValidationError>(
                    new ValidationError("Name required"), HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                return new HttpResponseMessage<ValidationError>(
                    new ValidationError("Category required"), HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public HttpResponseMessage Put(int id, Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return new HttpResponseMessage<ValidationError>(
                    new ValidationError("Name required"), HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                return new HttpResponseMessage<ValidationError>(
                    new ValidationError("Category required"), HttpStatusCode.BadRequest);
            }

            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            return existingProduct == null
                ? new HttpResponseMessage(HttpStatusCode.NotFound)
                : new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            return existingProduct == null
                ? new HttpResponseMessage(HttpStatusCode.NotFound)
                : new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
