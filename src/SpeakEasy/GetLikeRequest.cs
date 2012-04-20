using System.Net;

namespace SpeakEasy
{
    public abstract class GetLikeRequest : HttpRequest
    {
        protected GetLikeRequest(Resource resource)
            : base(resource)
        {

        }

        public override string BuildRequestUrl()
        {
            if (!Resource.HasParameters)
            {
                return Resource.Path;
            }

            var queryString = Resource.GetEncodedParameters();

            return string.Concat(Resource.Path, "?", queryString);
        }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);

            baseRequest.ContentType = transmissionSettings.DefaultSerializerContentType;

            return baseRequest;
        }
    }
}