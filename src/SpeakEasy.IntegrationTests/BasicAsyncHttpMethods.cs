using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SpeakEasy.IntegrationTests.Controllers;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [Collection("Api collection")]
    public class BasicAsyncHttpMethods
    {
        private readonly IHttpClient client;

        public BasicAsyncHttpMethods(ApiFixture fixture)
        {
            client = fixture.Client;
        }

        [Fact]
        public async void ShouldGetAsync()
        {
            var response = await client.Get("products/1");

            Assert.Contains(":1337/api/products/1", response.State.RequestUrl.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void ShouldGetCollection()
        {
            var products = await client.Get("products")
                .On(HttpStatusCode.OK)
                .As<List<Product>>();

            Assert.Equal(2, products.Count);
            Assert.Contains("Chocolate Cake", products.Select(p => p.Name));
        }

        [Fact]
        public async void ShouldGetCollectionWrongStatusCode()
        {
            await Assert.ThrowsAsync<HttpException>(() => client.Get("products").On(HttpStatusCode.Accepted).As<List<Product>>());
        }

        [Fact]
        public async void ShouldGetCollectionWithCustomConstructor()
        {
            var products = await client
                .Get("products")
                .On(HttpStatusCode.OK)
                .As(r => new List<Product> { new Product { Name = "Vanilla Cake" } });

            Assert.Contains("Vanilla Cake", products.Select(p => p.Name));
        }

        [Fact]
        public async void ShouldGetCollectionShort()
        {
            var products = await client
                .Get("products")
                .OnOk()
                .As<List<Product>>();

            Assert.Contains("Chocolate Cake", products.Select(p => p.Name));
        }

        [Fact]
        public async void ShouldGetProduct()
        {
            var product = await client
                .Get("products/1")
                .OnOk()
                .As<Product>();

            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async void ShouldGetProductWithSegments()
        {
            var product = await client
                .Get("products/:id", new { id = 1 })
                .OnOk()
                .As<Product>();

            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async void ShouldGetHead()
        {
            var success = await client.Head("products").IsOk();

            Assert.True(success);
        }

        [Fact]
        public async void ShouldGetOptions()
        {
            var success = await client.Options("products").IsOk();

            Assert.True(success);
        }

        [Fact]
        public async void ShouldCreateNewProduct()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var isok = await client
                .Post(product, "products")
                .Is(HttpStatusCode.Created);

            Assert.True(isok);
        }

        [Fact]
        public async void ShouldCreateNewProductShort()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var success = await client
                .Post(product, "products")
                .Is(HttpStatusCode.Created);

            Assert.True(success);
        }

        [Fact]
        public async void ShouldCreateNewProductShortWithErrorHandling()
        {
            var product = new Product { Name = "Canoli", Category = "Italian Treats" };

            var success = await client
                .Post(product, "products")
                .On(HttpStatusCode.BadRequest, (ValidationError e) => throw new ValidationException())
                .Is(HttpStatusCode.Created);

            Assert.True(success);
        }

        [Fact]
        public async void ShouldCreateNewProductWithErrors()
        {
            var product = new Product { Name = "Canoli", Category = "" };

            await Assert.ThrowsAsync<ValidationException>(() =>
                client.Post(product, "products")
                    .On(HttpStatusCode.BadRequest, (ValidationError e) => throw new ValidationException())
                    .OnOk(() => throw new Exception("Expected error"))
            );
        }

        [Fact]
        public async void ShouldUpdatePerson()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = await client
                .Put(product, "products/:id", new { id = 1 })
                .IsOk();

            Assert.True(success);
        }

        [Fact]
        public async void ShouldUpdateReservations()
        {
            var success = await client
                .Post("products/:id/reservations", new { id = 1 })
                .IsOk();

            Assert.True(success);
        }

        [Fact]
        public async void ShouldUpdatePersonUsingBodyAsSegmentProvider()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = await client
                .Put(product, "products/:id")
                .IsOk();

            Assert.True(success);
        }

        [Fact]
        public async void ShouldUpdateProductsWithPatch()
        {
            var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

            var success = await client
                .Patch(product, "products/:id")
                .IsOk();

            Assert.True(success);
        }

        [Fact]
        public async void ShouldUpdatePersonWithErrors()
        {
            var product = new Product { Id = 1, Name = "", Category = "Cakes" };

            await Assert.ThrowsAsync<ValidationException>(() =>
                client.Put(product, "products/:id", new { id = 1 })
                    .On(HttpStatusCode.BadRequest, (ValidationError e) => throw new ValidationException())
            );
        }

        [Fact]
        public async void ShouldDeletePerson()
        {
            var success = await client.Delete("products/:id", new { id = 1 })
                .On(HttpStatusCode.NotFound, () => throw new Exception("Could not find person to delete"))
                .Is(HttpStatusCode.NoContent);

            Assert.True(success);
        }

        [Fact]
        public async void ShouldBeAbleToUseNumericResponseCodes()
        {
            var success = await client
                .Post("search", new { username = "unknown-username" })
                .Is(422);

            Assert.True(success);
        }

        [Fact]
        public async void ShouldDeserializeCollectionAsObject()
        {
            var obj = await client
                .Get("products")
                .On(HttpStatusCode.OK)
                .As(typeof(List<Product>));

            var products = (List<Product>)obj;

            Assert.Contains("Chocolate Cake", products.Select(p => p.Name));
        }

        [Fact]
        public async void ShouldCallbackWithState()
        {
            var message = string.Empty;

            await client.Post("locations")
                .On(HttpStatusCode.BadRequest, status => { message = status.StatusDescription; });

            Assert.Equal("titles cannot start with 'bad'", message);
        }

        [Fact]
        public async void ShouldUseAdditionalSegmentsAsQueryParamsWhenBodySpecified()
        {
            var success = await client
                .Put(new { }, "products/:id/reservations", new { id = 1, priceIncrease = 500 })
                .Is(HttpStatusCode.Created);

            Assert.True(success);
        }

        [Fact]
        public async void ShouldGetAsyncString()
        {
            var response = await client
                .Get("products/overview")
                .OnOk()
                .As<string>();

            Assert.Equal("a super string", response);
        }
    }
}
