using System.Net;

namespace Resticle
{
    public class DeleteRestRequest : RestRequest
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