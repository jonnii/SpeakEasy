using System.Net;
using System.Text;

namespace Resticle
{
    public abstract class PostLikeRestRequest : RestRequest
    {
        protected PostLikeRestRequest(Resource resource)
            : base(resource)
        {
        }

        protected PostLikeRestRequest(Resource resource, object body)
            : base(resource)
        {
            Body = body;
        }

        public object Body { get; set; }

        public bool HasBody
        {
            get { return Body != null; }
        }

        public override HttpWebRequest BuildWebRequest(ITransmission transmission)
        {
            var baseRequest = base.BuildWebRequest(transmission);
            baseRequest.Method = GetHttpMethod();

            if (HasBody)
            {
                var serializer = transmission.DefaultSerializer;

                var serialized = serializer.Serialize(Body);
                var bytes = Encoding.Default.GetBytes(serialized);

                baseRequest.ContentLength = bytes.Length;
                using (var stream = baseRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            return baseRequest;
        }

        protected abstract string GetHttpMethod();

        public override string BuildRequestUrl(Resource resource)
        {
            return resource.Path;
        }
    }
}