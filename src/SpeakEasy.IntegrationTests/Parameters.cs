using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;

namespace SpeakEasy.IntegrationTests
{
    [TestFixture]
    public class QueryStringAndFormParameters : WithApi
    {
        [Test]
        public void ShouldAddQueryStringParametersOnGet()
        {
            var response = client.Get("search", new { filter = "c" });

            var products = response.OnOk().As<IEnumerable<string>>();

            Assert.That(products.First(), Is.EqualTo("cake"));
        }

        [Test]
        public void ShouldMergeParametersAndUseExtraParametersAsQueryStringParameters()
        {
            var response = client.Get("search/:category", new { category = "top100", filter = "c" });

            var products = response.OnOk().As<IEnumerable<string>>();

            Assert.That(products.First(), Is.EqualTo("cake"));
        }

        [Test]
        public void ShouldPostParameters()
        {
            var response = client.Post("search", new { username = "bob" });

            var user = response.On(HttpStatusCode.Created).As<string>();

            Assert.That(user, Is.EqualTo("bob"));
        }
    }
}