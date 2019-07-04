using System.Linq;
using System.Text;
using SpeakEasy.Contents;

namespace SpeakEasy.Bodies
{
    internal class PostRequestBody : IRequestBody
    {
        private readonly Resource resource;

        public PostRequestBody(Resource resource)
        {
            this.resource = resource;
        }

        public bool ConsumesResourceParameters { get; } = true;

        public IContent Serialize(ITransmissionSettings transmissionSettings, IParameterFormatter arrayFormatter)
        {
            if (!resource.HasParameters)
            {
                return new ByteArrayContent(transmissionSettings.DefaultSerializerContentType, new byte[0]);
            }

            if (resource.Parameters.Any(p => p.Value is IFile))
            {
                return new MultipartFileFormDataContent(resource);
            }

            var parameters = resource.GetEncodedParameters(arrayFormatter);
            var content = Encoding.UTF8.GetBytes(parameters);

            return new ByteArrayContent("application/x-www-form-urlencoded", content);
        }
    }
}
