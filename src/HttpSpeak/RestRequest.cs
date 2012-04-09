using System.Collections.Generic;
using System.Net;

namespace HttpSpeak
{
    public abstract class RestRequest : IRestRequest
    {
        private readonly List<Header> headers = new List<Header>();

        protected RestRequest(Resource resource)
        {
            Resource = resource;
        }

        public Resource Resource { get; private set; }

        public string UserAgent { get; set; }

        public int NumHeaders
        {
            get { return headers.Count; }
        }

        public IEnumerable<Header> Headers
        {
            get { return headers; }
        }

        public virtual HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var url = BuildRequestUrl(Resource);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            ServicePointManager.Expect100Continue = false;

            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.ContentType = CalculateContentType(transmissionSettings);
            request.ContentLength = 0;

            if (!string.IsNullOrEmpty(UserAgent))
            {
                request.UserAgent = UserAgent;
            }

            foreach (var header in Headers)
            {
                request.Headers.Add(header.Name, header.Value);
            }

            return request;
        }

        public void AddHeader(string name, string value)
        {
            headers.Add(new Header(name, value));
        }

        protected abstract string BuildRequestUrl(Resource resource);

        protected abstract string CalculateContentType(ITransmissionSettings transmissionSettings);
    }
}