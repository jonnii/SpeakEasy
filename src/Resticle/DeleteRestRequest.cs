namespace Resticle
{
    public class DeleteRestRequest : RestRequest
    {
        public DeleteRestRequest(string url)
            : base(url)
        {
        }

        public override System.Net.WebRequest BuildWebRequest()
        {
            var baseRequest = base.BuildWebRequest();

            baseRequest.Method = "DELETE";

            return baseRequest;
        }
    }
}