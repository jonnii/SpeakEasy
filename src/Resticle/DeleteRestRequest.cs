using System.Net;

namespace Resticle
{
    public class DeleteRestRequest : RestRequest
    {
        public DeleteRestRequest(string url)
            : base(url)
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