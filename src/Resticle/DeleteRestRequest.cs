using System.Net;

namespace Resticle
{
    public sealed class DeleteRestRequest : GetLikeRestRequest
    {
        public DeleteRestRequest(Resource resource)
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