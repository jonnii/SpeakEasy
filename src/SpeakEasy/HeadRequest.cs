using System.Net;

namespace SpeakEasy
{
    public sealed class HeadRequest : GetLikeRequest
    {
        public HeadRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);

            baseRequest.Method = "HEAD";

            return baseRequest;
        }

        public override string ToString()
        {
            return string.Format("[HeadRequest {0}]", Resource);
        }
    }
}