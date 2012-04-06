using System.Net;

namespace Resticle
{
    public abstract class RestRequest : IRestRequest
    {
        protected RestRequest(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }

        public virtual HttpWebRequest BuildWebRequest(ITransmission transmission)
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);

            request.ContentType = transmission.ContentType;
            request.ContentLength = 0;
            request.Accept = string.Join(", ", transmission.DeserializableMediaTypes);

            return request;
        }
    }
}