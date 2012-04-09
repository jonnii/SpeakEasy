using System.Collections.Generic;

namespace HttpSpeak
{
    public sealed class PatchRequest : PostLikeRequest
    {
        public PatchRequest(Resource resource)
            : base(resource)
        {

        }

        public PatchRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        public PatchRequest(Resource resource, IEnumerable<FileUpload> files)
            : base(resource, files)
        {

        }

        protected override string GetHttpMethod()
        {
            return "PATCH";
        }
    }
}