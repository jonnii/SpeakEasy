using System.Net;

namespace Resticle
{
    public class HeadRestRequest : RestRequest
    {
        public HeadRestRequest(Resource resource)
            : base(resource)
        {

        }

        public override HttpWebRequest BuildWebRequest(ITransmission transmission)
        {
            var baseRequest = base.BuildWebRequest(transmission);

            baseRequest.Method = "HEAD";

            return baseRequest;
        }
    }
}