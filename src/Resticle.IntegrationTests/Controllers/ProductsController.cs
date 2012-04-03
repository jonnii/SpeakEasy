using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Resticle.IntegrationTests.Controllers
{
    public class ProductsController
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

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
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

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
