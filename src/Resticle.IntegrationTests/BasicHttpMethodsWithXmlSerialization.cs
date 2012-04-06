using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Resticle.Deserializers;
using Resticle.IntegrationTests.Controllers;
using Resticle.Serializers;

namespace Resticle.IntegrationTests
{
    [TestFixture]
    public class BasicHttpMethodsWithXmlSerialization : WithApi
    {
        protected override IRestClient CreateClient()
        {
            var settings = RestClientSettings.Default;
            settings.DefaultSerializer = new DotNetXmlSerializer();

            return RestClient.Create("http://localhost:1337/api", settings);
        }

        [Test]
        public void ShouldHaveCorrectDeserializerOnResponse()
        {
            var response = client.Get("products/1");

            Assert.That(response.Deserializer, Is.TypeOf<DotNetXmlDeserializer>());
        }

        [Test]
        public void ShouldCreateNewProduct()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var isok = client.Post(product, "products").Is(HttpStatusCode.Created);

            Assert.That(isok);
        }

        [Test]
        public void ShouldGetCollection()
        {
            var products = client.Get("products").On(HttpStatusCode.OK).Unwrap<List<Product>>();

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }
    }
}