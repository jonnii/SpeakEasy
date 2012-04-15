using System.Net;

namespace SpeakEasy
{
    public abstract class PostLikeRequest : HttpRequest
    {
        protected PostLikeRequest(Resource resource)
            : base(resource)
        {
            Body = new DefaultRequestBody(resource);
        }

        protected PostLikeRequest(Resource resource, IRequestBody body)
            : base(resource)
        {
            Body = body;
        }

        public IRequestBody Body { get; private set; }

        public override HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings)
        {
            var baseRequest = base.BuildWebRequest(transmissionSettings);
            baseRequest.Method = GetHttpMethod();

            var serializedBody = Body.Serialize(transmissionSettings);

            baseRequest.ContentType = serializedBody.ContentType;
            if (serializedBody.ContentLength != -1)
            {
                baseRequest.ContentLength = serializedBody.ContentLength;
            }

            if (serializedBody.HasContent)
            {
                using (var stream = baseRequest.GetRequestStream())
                {
                    serializedBody.WriteTo(stream);
                }
            }

            return baseRequest;
        }

        protected abstract string GetHttpMethod();

        protected override string BuildRequestUrl(Resource resource)
        {
            return resource.Path;
        }
    }
}