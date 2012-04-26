using System;
using System.IO;
using System.Net;

namespace SpeakEasy.Specifications.Fixtures
{
    internal static class HttpResponses
    {
        internal static HttpResponse Create(ISerializer serializer, Stream bodyStream, HttpStatusCode code)
        {
            var headers = new[]
                {
                    new Header("awesome-header", "value")
                };

            var cookies = new[]
                {
                    new Cookie("comment", new Uri("http://fribble.com"), true, "domain", true, DateTime.Now, true, "name", "path", "port", false, DateTime.Now, "value", 5)
                };

            return new HttpResponse(
                serializer,
                bodyStream,
                new HttpResponseState(code,
                    new Uri("http://example.com/companies"),
                    headers,
                    cookies,
                    "contentType"));
        }

    }
}
