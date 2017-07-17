using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SpeakEasy.IntegrationTests.Controllers;
using SpeakEasy.Serializers;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [Collection("Api collection")]
    public class BasicHttpMethods
    {
        private IHttpClient client;

        public BasicHttpMethods(ApiFixture fixture)
        {
            this.client = fixture.Client;
        } 
        
        [Fact]
        public void ShouldHaveCorrectPropertiesOnResponse()
        {
            var response = client.Get("products/1");

            // Assert.Equal(response.State.RequestUrl.ToString(), Does.EndWith(":1337/api/products/1"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Assert.Equal(response.Deserializer, Is.TypeOf<DefaultJsonSerializer>());
        }

        // public void ShouldGetHeadHurHur()
        // {
        //     var success = client.Head("products").IsOk();

        //     Assert.That(success);
        // }

        // public void ShouldGetOptions()
        // {
        //     var success = client.Options("products").IsOk();

        //     Assert.That(success);
        // }

        // public void ShouldGetCollection()
        // {
        //     var products = client.Get("products").On(HttpStatusCode.OK).As<List<Product>>();

        //     Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        // }

        // public void ShouldGetCollectionWrongStatusCode()
        // {
        //     Assert.Throws<HttpException>(() => client.Get("products").On(HttpStatusCode.Accepted).As<List<Product>>());
        // }

        // public void ShouldGetCollectionWithCustomConstructor()
        // {
        //     var products = client.Get("products").On(HttpStatusCode.OK).As(r => new List<Product> { new Product { Name = "Vanilla Cake" } });

        //     Assert.That(products.Any(p => p.Name == "Vanilla Cake"));
        // }

        // public void ShouldGetCollectionShort()
        // {
        //     var products = client.Get("products").OnOk().As<List<Product>>();

        //     Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        // }

        // public void ShouldGetProduct()
        // {
        //     var product = client.Get("products/1").OnOk().As<Product>();

        //     Assert.That(product.Id, Is.EqualTo(1));
        // }

        // public void ShouldGetProductWithSegments()
        // {
        //     var product = client.Get("products/:id", new { id = 1 }).OnOk().As<Product>();

        //     Assert.That(product.Id, Is.EqualTo(1));
        // }

        // public void ShouldCreateNewProduct()
        // {
        //     var product = new Product { Name = "Canoli", Category = "Italian Treats" };

        //     var isok = client.Post(product, "products").Is(HttpStatusCode.Created);

        //     Assert.That(isok);
        // }

        // public void ShouldCreateNewProductShort()
        // {
        //     var product = new Product { Name = "Canoli", Category = "Italian Treats" };

        //     var success = client.Post(product, "products").Is(HttpStatusCode.Created);

        //     Assert.That(success);
        // }

        // public void ShouldCreateNewProductShortWithtErrorHandling()
        // {
        //     var product = new Product { Name = "Canoli", Category = "Italian Treats" };

        //     var response = client.Post(product, "products");

        //     response.On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); });
        //     var success = response.Is(HttpStatusCode.Created);

        //     Assert.That(success);
        // }

        // public void ShouldCreateNewProductWithErrors()
        // {
        //     var product = new Product { Name = "Canoli", Category = "" };

        //     var response = client.Post(product, "products");

        //     Assert.Throws<ValidationException>(() =>
        //         response
        //             .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); })
        //             .OnOk(() => { throw new Exception("Expected error"); }));
        // }

        // public void ShouldUpdatePerson()
        // {
        //     var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

        //     var success = client.Put(product, "products/:id", new { id = 1 }).IsOk();

        //     Assert.That(success);
        // }

        // public void ShouldUpdateReservations()
        // {
        //     var success = client.Post("products/:id/reservations", new { id = 1 }).IsOk();

        //     Assert.That(success);
        // }

        // public void ShouldUpdatePersonUsingBodyAsSegmentProvider()
        // {
        //     var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

        //     var success = client.Put(product, "products/:id").IsOk();

        //     Assert.That(success);
        // }

        // public void ShouldUpdateProductsWithPatch()
        // {
        //     var product = new Product { Id = 1, Name = "Vanilla Cake", Category = "Cakes" };

        //     var success = client.Patch(product, "products/:id").IsOk();

        //     Assert.That(success);
        // }

        // public void ShouldUpdatePersonWithErrors()
        // {
        //     var product = new Product { Id = 1, Name = "", Category = "Cakes" };

        //     Assert.Throws<ValidationException>(() =>
        //         client.Put(product, "products/:id", new { id = 1 })
        //             .On(HttpStatusCode.BadRequest, (ValidationError e) => { throw new ValidationException(); }));
        // }

        // public void ShouldDeletePerson()
        // {
        //     var success = client.Delete("products/:id", new { id = 1 })
        //         .On(HttpStatusCode.NotFound, () => { throw new Exception("Could not find person to delete"); })
        //         .Is(HttpStatusCode.NoContent);

        //     Assert.That(success);
        // }

        // public void ShouldBeAbleToUseNumericResponseCodes()
        // {
        //     var response = client.Post("search", new { username = "unknown-username" });

        //     var success = response.Is(422);

        //     Assert.That(success);
        // }

        // public void ShouldDeserializeCollectionAsObject()
        // {
        //     var obj = client.Get("products").On(HttpStatusCode.OK).As(typeof(List<Product>));

        //     var products = (List<Product>)obj;

        //     Assert.That(products.Any(p => p.Name == "Chocolate Cake"));
        // }

        // public void ShouldCallbackWithState()
        // {
        //     var message = string.Empty;

        //     client.Post("locations")
        //         .On(HttpStatusCode.BadRequest, status => { message = status.StatusDescription; });

        //     Assert.That(message, Is.EqualTo("titles cannot start with 'bad'"));
        // }

        // public void ShouldUseAdditionalSegmentsAsQueryParamsWhenBodySpecified()
        // {
        //     var success = client.Put(new { }, "products/:id/reservations", new { id = 1, priceIncrease = 500 })
        //         .Is(HttpStatusCode.Created);

        //     Assert.That(success);
        // }
    }
}
