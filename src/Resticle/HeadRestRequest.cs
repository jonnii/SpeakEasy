using System.Net;

namespace Resticle
{
    public class HeadRestRequest : RestRequest
    {
        public HeadRestRequest(string url)
            : base(url)
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