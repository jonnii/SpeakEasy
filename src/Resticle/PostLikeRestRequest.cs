using System;
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

        public bool HasSerializableBody
        {
            get { return Body != null || Resource.HasParameters; }
        }

        public override HttpWebRequest BuildWebRequest(ITransmission transmission)
        {
            var baseRequest = base.BuildWebRequest(transmission);
            baseRequest.Method = GetHttpMethod();

            if (HasSerializableBody)
            {
                var serialized = GetSerializedBody(transmission);
                var bytes = Encoding.Default.GetBytes(serialized);

                baseRequest.ContentLength = bytes.Length;

                using (var stream = baseRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            return baseRequest;
        }

        private string GetSerializedBody(ITransmission transmission)
        {
            if (Body != null)
            {
                var serializer = transmission.DefaultSerializer;
                return serializer.Serialize(Body);
            }

            if (Resource.HasParameters)
            {
                return Resource.GetEncodedParameters();
            }

            throw new NotSupportedException(
                "Something has gone wrong... trying to get serialized body of a request when the request has nothing worth serializing");
        }

        protected abstract string GetHttpMethod();

        protected override string BuildRequestUrl(Resource resource)
        {
            return resource.Path;
        }

        protected override string CalculateContentType(ITransmission transmission)
        {
            return Resource.HasParameters
                ? "application/x-www-form-urlencoded"
                : transmission.ContentType;
        }
    }
}