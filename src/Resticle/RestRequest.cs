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
            var request = (HttpWebRequest)WebRequest.Create(Resource.Path);

            request.ContentType = transmission.ContentType;
            request.ContentLength = 0;
            request.Accept = string.Join(", ", transmission.DeserializableMediaTypes);

            return request;
        }
    }
}