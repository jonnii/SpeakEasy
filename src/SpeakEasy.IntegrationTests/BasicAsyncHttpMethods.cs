using System.Net;
using NUnit.Framework;
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
    }
}