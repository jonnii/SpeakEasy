using System.IO;
using System.Text;

namespace HttpSpeak
{
    public class NullRequestBody : IRequestBody
    {
        private readonly Resource resource;

        public NullRequestBody(Resource resource)
        {
            this.resource = resource;
        }

        public byte[] SerializeToByteArray(ITransmissionSettings transmissionSettings)
        {
            var parameters = resource.GetEncodedParameters();

            var bytes = Encoding.Default.GetBytes(parameters);

            return bytes;
        }
    }
}