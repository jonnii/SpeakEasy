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

        public virtual HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var url = BuildRequestUrl(Resource);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = false;
            ServicePointManager.Expect100Continue = false;

            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.ContentType = CalculateContentType(transmissionSettings);
            request.ContentLength = 0;
            request.Accept = string.Join(", ", transmissionSettings.DeserializableMediaTypes);

            return request;
        }

        protected abstract string BuildRequestUrl(Resource resource);

        protected abstract string CalculateContentType(ITransmissionSettings transmissionSettings);
    }
}