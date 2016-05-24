using System.Text;
using SpeakEasy.Bodies;

namespace SpeakEasy
{
    internal class PostRequestBody : IRequestBody
    {
        private readonly Resource resource;

        public PostRequestBody(Resource resource)
        {
            this.resource = resource;
        }

        public bool ConsumesResourceParameters
        {
            get { return true; }
        }

        public IContent Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            if (resource.HasParameters)
            {
                var parameters = resource.GetEncodedParameters(arrayFormatter);
                var content = Encoding.UTF8.GetBytes(parameters);

                return new ByteArrayContent("application/x-www-form-urlencoded", content);
            }

            return new ByteArrayContent(transmissionSettings.DefaultSerializerContentType, new byte[0]);
        }
    }
}
