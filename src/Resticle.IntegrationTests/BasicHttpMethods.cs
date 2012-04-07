using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Resticle.Deserializers;
using Resticle.IntegrationTests.Controllers;

namespace Resticle.IntegrationTests
{
    [TestFixture]
    public class BasicHttpMethods : WithApi
    {
        [Test]
        public void ShouldHaveCorrectPropertiesOnResponse()
        {
            var response = client.Get("products/1");

            Assert.That(response.RequestedUrl.ToString(), Is.EqualTo("http://localhost:1337/api/products/1"));
            Assert.That(response.HttpStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Deserializer, Is.TypeOf<JsonDeserializer>());
        }

        [Test]
        public void ShouldGetHeadHurHur()
        {
            var success = client.Head("products").IsOk();

            Assert.That(success);
        }

        [Test]
        public void ShouldGetOptions()
        {
            var success = client.Options("products").IsOk();

            Assert.That(success);
        }

        [Test]
        public void ShouldGetCollection()
        {
            var products = client.Get("products").On(HttpStatusCode.OK).Unwrap<List<Product>>();

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldGetCollectionShort()
        {
            var products = client.Get("products").OnOk().Unwrap<List<Product>>();

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldGetProduct()
        {
            var product = client.Get("products/1").OnOk().Unwrap<Product>();

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetProductWithSegments()
        {
            var product = client.Get("products/:id", new { id = 1 }).OnOk().Unwrap<Product>();

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetProductWithResource()
        {
            var resource = new Resource("products/:id");

            var product = client.Get(resource.Merge(new { id = 1 })).OnOk().Unwrap<Product>();

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetProductWithDynamicResource()
        {
            var resource = Resource.Create("products/:id");

            var product = client.Get(resource.Id(1)).OnOk().Unwrap<Product>();

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldCreateNewProduct()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var isok = client.Post(product, "products").Is(HttpStatusCode.Created);

            Assert.That(isok);
        }

        [Test]
        public void ShouldCreateNewProductShort()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var success = client.Post(product, "products").Is(HttpStatusCode.Created);

            Assert.That(success);
        }

        [Test]
        public void ShouldCreateNewProductWithErrors()
        {
            var product = new Product { Name = "Canoli", Category = "" };

            var response = client.Post(product, "products");

            Assert.Throws<ValidationException>(() =>
                response
                    .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); })
                    .OnOk(() => { throw new Exception("Expected error"); }));
        }

        [Test]
        public void ShouldUpdatePerson()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = client.Put(product, "products/:id", new { id = 1 }).IsOk();

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdateReservations()
        {
            var success = client.Post("products/:id/reservations", new { id = 1 }).IsOk();

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdatePersonUsingBodyAsSegmentProvider()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = client.Put(product, "products/:id").IsOk();

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdatePersonWithErrors()
        {
            var product = new Product { Id = 1, Name = "", Category = "Cakes" };

            Assert.Throws<ValidationException>(() =>
                client.Put(product, "products/:id", new { id = 1 })
                    .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); }));
        }

        [Test]
        public void ShouldDeletePerson()
        {
            var success = client.Delete("products/:id", new { id = 1 })
                .On(HttpStatusCode.NotFound, () => { throw new Exception("Could not find person to delete"); })
                .Is(HttpStatusCode.NoContent);

            Assert.That(success);
        }
    }
}