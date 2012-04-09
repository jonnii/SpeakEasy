using System.Collections.Generic;

namespace HttpSpeak
{
    public sealed class PatchRestRequest : PostLikeRestRequest
    {
        public PatchRestRequest(Resource resource)
            : base(resource)
        {

        }

        public PatchRestRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        public PatchRestRequest(Resource resource, IEnumerable<FileUpload> files)
            : base(resource, files)
        {

        }

        protected override string GetHttpMethod()
        {
            return "PATCH";
        }
    }
}