using System.Collections.Generic;
using System.Net;

namespace SpeakEasy
{
    public abstract class HttpRequest : IHttpRequest
    {
        private readonly List<Header> headers = new List<Header>();

        protected HttpRequest(Resource resource, IRequestBody body)
        {
            Resource = resource;
            Body = body;
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

        public HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var url = BuildRequestUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            ServicePointManager.Expect100Continue = false;

            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.Credentials = Credentials;
            request.Method = HttpMethod;

            if (!string.IsNullOrEmpty(UserAgent))
            {
                request.UserAgent = UserAgent;
            }

            foreach (var header in Headers)
            {
                request.Headers.Add(header.Name, header.Value);
            }

            var serializedBody = Body.Serialize(transmissionSettings);

            request.ContentType = serializedBody.ContentType;
            if (serializedBody.ContentLength != -1)
            {
                request.ContentLength = serializedBody.ContentLength;
            }

            if (serializedBody.HasContent)
            {
                using (var stream = request.GetRequestStream())
                {
                    serializedBody.WriteTo(stream);
                }
            }

            return request;
        }

        public void AddHeader(string name, string value)
        {
            headers.Add(new Header(name, value));
        }

        public abstract string BuildRequestUrl();

        public abstract override string ToString();
    }
}