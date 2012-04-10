using System.Net;

namespace HttpSpeak
{
    public abstract class PostLikeRequest : HttpRequest
    {
        protected PostLikeRequest(Resource resource)
            : base(resource)
        {
            Body = new NullRequestBody(resource);
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

            var bytes = Body.SerializeToByteArray(transmissionSettings);

            if (bytes.Length > 0)
            {
                baseRequest.ContentLength = bytes.Length;

                using (var stream = baseRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            return baseRequest;
        }

        protected abstract string GetHttpMethod();

        protected override string BuildRequestUrl(Resource resource)
        {
            return resource.Path;
        }

        protected override string CalculateContentType(ITransmissionSettings transmissionSettings)
        {
            return Resource.HasParameters
                ? "application/x-www-form-urlencoded"
                : transmissionSettings.DefaultSerializerContentType;
        }
    }
}