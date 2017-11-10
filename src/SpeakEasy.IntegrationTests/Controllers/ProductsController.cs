using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/products")]
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
        public IActionResult Head()
        {
            return Ok();
        }

        [AcceptVerbs("OPTIONS")]
        public IActionResult Options()
        {
            return Ok();
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public Product GetProduct(int id)
        {
            return products.Single(p => p.Id == id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest(new ValidationError("Product required"));
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest(new ValidationError("Name required"));
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                return BadRequest(new ValidationError("Category required"));
            }

            return CreatedAtRoute(nameof(GetProduct), new { id = 33 }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest(new ValidationError("Name required"));
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                return BadRequest(new ValidationError("Category required"));
            }

            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            return existingProduct == null
                ? (IActionResult)NotFound()
                : Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest(new ValidationError("Name required"));
            }

            if (string.IsNullOrEmpty(product.Category))
            {
                return BadRequest(new ValidationError("Category required"));
            }

            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            return existingProduct == null
                ? (IActionResult)NotFound()
                : Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            return existingProduct == null
                ? (IActionResult)NotFound()
                : NoContent();
        }
    }
}
