using System.Net;

namespace HttpSpeak
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
    }
}