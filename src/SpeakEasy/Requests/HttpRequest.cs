using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace SpeakEasy.Requests
{
    internal abstract class HttpRequest : IHttpRequest
    {
        private readonly List<Header> headers = new List<Header>();

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

        public int NumHeaders => headers.Count;

        public IEnumerable<Header> Headers => headers;

        public ICredentials Credentials { get; set; }

        public bool AllowAutoRedirect { get; set; }

        public CookieContainer CookieContainer { get; set; }

        public void AddHeader(string name, string value)
        {
            headers.Add(new Header(name, value));
        }

        public string BuildRequestUrl(IArrayFormatter arrayFormatter)
        {
            if (!Resource.HasParameters || Body.ConsumesResourceParameters)
            {
                return Resource.Path;
            }

            var queryString = Resource.GetEncodedParameters(arrayFormatter);

            return string.Concat(Resource.Path, "?", queryString);
        }
    }
}
