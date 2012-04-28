using System.Collections.Generic;
using System.Net;

namespace SpeakEasy
{
    public abstract partial class HttpRequest : IHttpRequest
    {
        private readonly List<Header> headers = new List<Header>();

        protected HttpRequest(Resource resource, IRequestBody body)
        {
            Resource = resource;
            Body = body;

            AllowAutoRedirect = true;
        }

        public Resource Resource { get; private set; }

        public IRequestBody Body { get; private set; }

        public string UserAgent { get; set; }

        public abstract string HttpMethod { get; }

        public int NumHeaders
        {
            get { return headers.Count; }
        }

        public IEnumerable<Header> Headers
        {
            get { return headers; }
        }

        public ICredentials Credentials { get; set; }

        public bool AllowAutoRedirect { get; set; }

        public void AddHeader(string name, string value)
        {
            headers.Add(new Header(name, value));
        }

        public abstract string BuildRequestUrl();

        public abstract override string ToString();
    }
}