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
            var didComplete = false;

            var task = client.GetAsync("products/1")
                .OnComplete(response =>
                {
                    didComplete = true;

                    Assert.That(response.State.RequestUrl.ToString(), Is.EqualTo("http://localhost:1337/api/products/1"));
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(response.Deserializer, Is.TypeOf<JsonDotNetSerializer>());
                }).Start();

            task.Wait();

            Assert.That(didComplete);
        }
    }
}