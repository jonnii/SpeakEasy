using System.Text;

namespace SpeakEasy
{
    internal class PostRequestBody : IRequestBody
    {
        private readonly Resource resource;

        public PostRequestBody(Resource resource)
        {
            this.resource = resource;
        }

        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings)
        {
            if (resource.HasParameters)
            {
                var parameters = resource.GetEncodedParameters();
                var content = Encoding.UTF8.GetBytes(parameters);

                return new SerializableByteArray("application/x-www-form-urlencoded", content);
            }

            return new SerializableByteArray(transmissionSettings.DefaultSerializerContentType, new byte[0]);
        }
    }
}