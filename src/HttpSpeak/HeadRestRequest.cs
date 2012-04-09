using System.Net;

namespace HttpSpeak
{
    public sealed class HeadRestRequest : GetLikeRestRequest
    {
        public HeadRestRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);

            baseRequest.Method = "HEAD";

            return baseRequest;
        }
    }
}