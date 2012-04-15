using System.Net;

namespace SpeakEasy
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

        public override string ToString()
        {
            return string.Format("[OptionsRequest {0}]", Resource);
        }
    }
}