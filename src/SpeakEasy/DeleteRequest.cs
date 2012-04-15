using System.Net;

namespace SpeakEasy
{
    public sealed class DeleteRequest : GetLikeRequest
    {
        public DeleteRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);

            baseRequest.Method = "DELETE";

            return baseRequest;
        }

        public override string ToString()
        {
            return string.Format("[DeleteRequest {0}]", Resource);
        }
    }
}