using System.Collections.Generic;

namespace HttpSpeak
{
    public sealed class PostRequest : PostLikeRequest
    {
        public PostRequest(Resource resource)
            : base(resource)
        {

        }

        public PostRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        public PostRequest(Resource resource, IEnumerable<FileUpload> files)
            : base(resource, files)
        {

        }

        protected override string GetHttpMethod()
        {
            return "POST";
        }
    }
}