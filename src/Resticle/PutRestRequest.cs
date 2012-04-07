using System.Collections.Generic;

namespace Resticle
{
    public sealed class PutRestRequest : PostLikeRestRequest
    {
        public PutRestRequest(Resource resource)
            : base(resource)
        {

        }

        public PutRestRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        public PutRestRequest(Resource resource, IEnumerable<FileUpload> files)
            : base(resource, files)
        {

        }

        protected override string GetHttpMethod()
        {
            return "PUT";
        }
    }
}