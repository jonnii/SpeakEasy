using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SpeakEasy.Requests
{
    internal abstract class HttpRequest : IHttpRequest
    {
        protected HttpRequest(Resource resource, IRequestBody body)
        {
            Resource = resource;
            Body = body;
        }

        public Resource Resource { get; }

        public IRequestBody Body { get; }

        public List<Action<HttpRequestHeaders>> Headers { get; } = new List<Action<HttpRequestHeaders>>();

        public abstract HttpMethod HttpMethod { get; }

        public string BuildRequestUrl(IQuerySerializer querySerializer)
        {
            if (!Resource.HasParameters || Body.ConsumesResourceParameters)
            {
                return Resource.Path;
            }

            var queryString = Resource.GetEncodedParameters(querySerializer);

            return string.Concat(Resource.Path, "?", queryString);
        }

        public void AddHeader(string header, string value)
        {
            Headers.Add(h => h.Add(header, value));
        }

        public void AddHeader(Action<HttpRequestHeaders> headers)
        {
            Headers.Add(headers);
        }
    }
}
