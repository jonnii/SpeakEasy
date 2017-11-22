using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace SpeakEasy.Requests
{
    internal abstract class HttpRequest : IHttpRequest
    {
        protected HttpRequest(Resource resource, IRequestBody body)
        {
            Resource = resource;
            Body = body;

            AllowAutoRedirect = true;
        }

        public Resource Resource { get; }

        public IRequestBody Body { get; }

        public IWebProxy Proxy { get; set; }

        public X509CertificateCollection ClientCertificates { get; set; }

        public int? MaximumAutomaticRedirections { get; set; }

        public abstract HttpMethod HttpMethod { get; }

        public ICredentials Credentials { get; set; }

        public bool AllowAutoRedirect { get; set; }

        public CookieContainer CookieContainer { get; set; }

        public string BuildRequestUrl(IArrayFormatter arrayFormatter)
        {
            if (!Resource.HasParameters || Body.ConsumesResourceParameters)
            {
                return Resource.Path;
            }

            var queryString = Resource.GetEncodedParameters(arrayFormatter);

            return string.Concat(Resource.Path, "?", queryString);
        }

        public List<Action<HttpRequestHeaders>> Headers { get; } = new List<Action<HttpRequestHeaders>>();

        public void AddHeader(string header, string value)
        {
            Headers.Add(f => f.Add(header, value));
        }

        public void AddHeader(Action<HttpRequestHeaders> headers)
        {
            Headers.Add(headers);
        }
    }
}
