using System.Collections.Generic;

namespace Resticle
{
    public sealed class PostRestRequest : PostLikeRestRequest
    {
        public PostRestRequest(Resource resource)
            : base(resource)
        {

        }

        public PostRestRequest(Resource resource, object body)
            : base(resource, body)
        {

        }

        public PostRestRequest(Resource resource, IEnumerable<FileUpload> files)
            : base(resource, files)
        {

        }

        protected override string GetHttpMethod()
        {
            return "POST";
        }
    }
}