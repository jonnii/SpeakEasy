using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using SpeakEasy.IntegrationTests.Controllers;
using SpeakEasy.IntegrationTests.Extensions;
using SpeakEasy.Serializers;

namespace SpeakEasy.IntegrationTests
{
    [TestFixture]
    public class BasicAsyncHttpMethods : WithApi
    {
        [Test]
        public void ShouldGetAsync()
        {
            var request = client.GetAsync("products/1");

            var response = request.Result;

            Assert.That(response.State.RequestUrl.ToString(), Is.StringEnding(":1337/api/products/1"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Deserializer, Is.TypeOf<DefaultJsonSerializer>());
        }

        [Test]
        public void ShouldGetCollection()
        {
            var products = client.GetAsync("products").On(HttpStatusCode.OK).As<List<Product>>().Result;

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldGetCollectionWrongStatusCode()
        {
            Assert.Throws<HttpException>(() => client.GetAsync("products").On(HttpStatusCode.Accepted).As<List<Product>>().Await());
        }

        [Test]
        public void ShouldGetCollectionWithCustomConstructor()
        {
            var products = client.GetAsync("products").On(HttpStatusCode.OK).As(r => new List<Product> { new Product { Name = "Vanilla Cake" } }).Result;

            Assert.That(products.Any(p => p.Name == "Vanilla Cake"));
        }

        [Test]
        public void ShouldGetCollectionShort()
        {
            var products = client.GetAsync("products").OnOk().As<List<Product>>().Result;

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldGetProduct()
        {
            var product = client.GetAsync("products/1").OnOk().As<Product>().Result;

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetProductWithSegments()
        {
            var product = client.GetAsync("products/:id", new { id = 1 }).OnOk().As<Product>().Result;

            Assert.That(product.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldCreateNewProduct()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var isok = client.PostAsync(product, "products").Is(HttpStatusCode.Created).Result;

            Assert.That(isok);
        }

        [Test]
        public void ShouldCreateNewProductShort()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var success = client.PostAsync(product, "products").Is(HttpStatusCode.Created).Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldCreateNewProductShortWithtErrorHandling()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var response = client.PostAsync(product, "products");

            var success = response
                .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); })
                .Is(HttpStatusCode.Created)
                .Result;

            Assert.That(success, Is.True);
        }

        [Test]
        public void ShouldCreateNewProductWithErrors()
        {
            var product = new Product { Name = "Canoli", Category = "" };

            var response = client.PostAsync(product, "products");

            Assert.Throws<ValidationException>(() =>
                response
                    .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); })
                    .OnOk(() => { throw new Exception("Expected error"); })
                    .Await());
        }

        [Test]
        public void ShouldUpdatePerson()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = client.PutAsync(product, "products/:id", new { id = 1 }).IsOk().Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdateReservations()
        {
            var success = client.PostAsync("products/:id/reservations", new { id = 1 }).IsOk().Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdatePersonUsingBodyAsSegmentProvider()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = client.PutAsync(product, "products/:id").IsOk().Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdateProductsWithPatch()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = client.PatchAsync(product, "products/:id").IsOk().Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldUpdatePersonWithErrors()
        {
            var product = new Product { Id = 1, Name = "", Category = "Cakes" };

            Assert.Throws<ValidationException>(() =>
                client.PutAsync(product, "products/:id", new { id = 1 })
                    .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); })
                    .Await());
        }

        [Test]
        public void ShouldDeletePerson()
        {
            var success = client.DeleteAsync("products/:id", new { id = 1 })
                .On(HttpStatusCode.NotFound, () => { throw new Exception("Could not find person to delete"); })
                .Is(HttpStatusCode.NoContent)
                .Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldBeAbleToUseNumericResponseCodes()
        {
            var response = client.PostAsync("search", new { username = "unknown-username" });

            var success = response.Is(422).Result;

            Assert.That(success);
        }

        [Test]
        public void ShouldDeserializeCollectionAsObject()
        {
            var obj = client.GetAsync("products").On(HttpStatusCode.OK).As(typeof(List<Product>));

            var products = (List<Product>)obj.Result;

            Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        }

        [Test]
        public void ShouldCallbackWithState()
        {
            var message = string.Empty;

            client.PostAsync("locations")
                .On(HttpStatusCode.BadRequest, status => { message = status.StatusDescription; })
                .Await();

            Assert.That(message, Is.EqualTo("titles cannot start with 'bad'"));
        }

        [Test]
        public void ShouldUseAdditionalSegmentsAsQueryParamsWhenBodySpecified()
        {
            var success = client.PutAsync(new { }, "products/:id/reservations", new { id = 1, priceIncrease = 500 })
                .Is(HttpStatusCode.Created)
                .Result;

            Assert.That(success);
        }
    }
}
