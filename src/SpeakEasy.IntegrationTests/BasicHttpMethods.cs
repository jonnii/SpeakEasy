using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using SpeakEasy.IntegrationTests.Controllers;
using SpeakEasy.Serializers;

namespace SpeakEasy.IntegrationTests
{
    [TestFixture]
    public class BasicHttpMethods : WithApi
    {
        [Test]
        public void ShouldHaveCorrectPropertiesOnResponse()
        {
            var response = client.Get("products/1");

            Assert.That(response.State.RequestUrl.ToString(), Is.StringEnding(":1337/api/products/1"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Deserializer, Is.TypeOf<DefaultJsonSerializer>());
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
            var products = client.Get("products").On(HttpStatusCode.OK).As<List<Product>>();

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldGetCollectionWrongStatusCode()
        {
            Assert.Throws<HttpException>(() => client.Get("products").On(HttpStatusCode.Accepted).As<List<Product>>());
        }

        [Test]
        public void ShouldGetCollectionWithCustomConstructor()
        {
            var products = client.Get("products").On(HttpStatusCode.OK).As(r => new List<Product> { new Product { Name = "Vanilla Cake" } });

            Assert.That(products.Any(p => p.Name == "Vanilla Cake"));
        }

        [Test]
        public void ShouldGetCollectionShort()
        {
            var products = client.Get("products").OnOk().As<List<Product>>();

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldGetProduct()
        {
            var product = client.Get("products/1").OnOk().As<Product>();

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetProductWithSegments()
        {
            var product = client.Get("products/:id", new { id = 1 }).OnOk().As<Product>();

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
        public void ShouldCreateNewProductShortWithtErrorHandling()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var response = client.Post(product, "products");

            response.On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); });
            var success = response.Is(HttpStatusCode.Created);

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
        public void ShouldUpdateProductsWithPatch()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = client.Patch(product, "products/:id").IsOk();

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

        [Test]
        public void ShouldBeAbleToUseNumericResponseCodes()
        {
            var response = client.Post("search", new { username = "unknown-username" });

            var success = response.Is(422);

            Assert.That(success);
        }

        [Test]
        public void ShouldDeserializeCollectionAsObject()
        {
            var obj = client.Get("products").On(HttpStatusCode.OK).As(typeof(List<Product>));

            var products = (List<Product>)obj;

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }
    }
}