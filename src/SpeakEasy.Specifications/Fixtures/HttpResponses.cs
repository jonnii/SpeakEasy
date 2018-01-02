using System.IO;
using System.Net;
using System.Net.Http;

namespace SpeakEasy.Specifications.Fixtures
{
    public static class HttpResponses
    {
        public static HttpResponse Create(ISerializer serializer, Stream bodyStream, HttpStatusCode code)
        {
            var cookies = new[]
            {
                new Cookie("foo", "bob")
            };

            return new HttpResponse(
                serializer,
                bodyStream,
                new HttpResponseState(
                    new HttpResponseMessage { StatusCode = code },
                    cookies,
                    "contentType"), null);
        }
    }
}
