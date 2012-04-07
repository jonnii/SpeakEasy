using System.Net;

namespace Resticle
{
    public abstract class RestRequest : IRestRequest
    {
        protected RestRequest(Resource resource)
        {
            Resource = resource;
        }

        public Resource Resource { get; private set; }

        public virtual HttpWebRequest BuildWebRequest(ITransmission transmission)
        {
            var url = BuildRequestUrl(Resource);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = CalculateContentType(transmission);
            request.ContentLength = 0;
            request.Accept = string.Join(", ", transmission.DeserializableMediaTypes);

            return request;
        }

        protected abstract string BuildRequestUrl(Resource resource);

        protected abstract string CalculateContentType(ITransmission transmission);
    }
}