using System.Net;

namespace Resticle
{
    public sealed class DeleteRestRequest : GetLikeRestRequest
    {
        public DeleteRestRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpWebRequest BuildWebRequest(ITransmission transmission)
        {
            var baseRequest = base.BuildWebRequest(transmission);

            baseRequest.Method = "DELETE";

            return baseRequest;
        }
    }
}