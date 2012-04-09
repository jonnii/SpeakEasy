using System.Net;

namespace HttpSpeak
{
    public sealed class OptionsRequest : GetLikeRequest
    {
        public OptionsRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);

            baseRequest.Method = "OPTIONS";

            return baseRequest;
        }
    }
}