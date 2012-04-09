using System.Collections.Generic;

namespace HttpSpeak
{
    public sealed class PutRequest : PostLikeRequest
    {
        public PutRequest(Resource resource)
            : base(resource)
        {

        }

        public PutRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        public PutRequest(Resource resource, IEnumerable<FileUpload> files)
            : base(resource, files)
        {

        }

        protected override string GetHttpMethod()
        {
            return "PUT";
        }
    }
}