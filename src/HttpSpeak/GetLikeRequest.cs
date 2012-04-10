using System.Net;

namespace HttpSpeak
{
    public abstract class GetLikeRequest : HttpRequest
    {
        protected GetLikeRequest(Resource resource)
            : base(resource)
        {

        }

        protected override string BuildRequestUrl(Resource resource)
        {
            if (!resource.HasParameters)
            {
                return resource.Path;
            }

            var queryString = resource.GetEncodedParameters();

            return string.Concat(resource.Path, "?", queryString);
        }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);

            baseRequest.ContentType = transmissionSettings.DefaultSerializerContentType;

            return baseRequest;
        }
    }
}