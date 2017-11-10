using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [Collection("Api collection")]
    public class QueryStringAndFormParameters
    {
        private readonly IHttpClient client;

        public QueryStringAndFormParameters(ApiFixture fixture)
        {
            client = fixture.Client;
        }

        [Fact]
        public void ShouldAddQueryStringParametersOnGet()
        {
            var products = client
                .Get("search", new { filter = "c" })
                .OnOk()
                .As<IEnumerable<SearchResult>>();

            Assert.Equal("cake", products.First().Name);
        }

        [Fact]
        public void ShouldMergeParametersAndUseExtraParametersAsQueryStringParameters()
        {
            var products = client
                .Get("search/:category", new { category = "top100", filter = "c" })
                .OnOk()
                .As<IEnumerable<SearchResult>>();

            var result = products.First();

            Assert.Equal("cake", result.Name);
            Assert.Equal("top100", result.Category);
            Assert.Equal("c", result.Filter);
        }

        [Fact]
        public void ShouldPostParameters()
        {
            var user = client
                .Post("search", new { username = "bob" })
                .On(HttpStatusCode.Created)
                .As<SearchResult>();

            Assert.Equal("bob", user.Name);
        }

        public class SearchResult
        {
            public string Name { get; set; }

            public string Category { get; set; }

            public string Filter { get; set; }
        }
    }
}
