using System.Net;

namespace Resticle
{
    public sealed class OptionsRestRequest : GetLikeRestRequest
    {
        public OptionsRestRequest(Resource resource)
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