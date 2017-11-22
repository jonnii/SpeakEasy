using System;
using System.IO;
using System.Net;

namespace SpeakEasy.Specifications.Fixtures
{
    public static class HttpResponses
    {
        public static HttpResponse Create(ISerializer serializer, Stream bodyStream, HttpStatusCode code)
        {
            var cookies = new[]
            {
                new Cookie("foo", "bob"),
            };

            return new HttpResponse(
                serializer,
                bodyStream,
                new HttpResponseState(code,
                    "status description",
                    new Uri("http://example.com/companies"),
                    cookies,
                    "contentType",
                    "server",
                    null), null);
        }
    }
}
