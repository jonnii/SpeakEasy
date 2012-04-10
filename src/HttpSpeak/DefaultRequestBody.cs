using System.Text;

namespace HttpSpeak
{
    public class DefaultRequestBody : IRequestBody
    {
        private readonly Resource resource;

        public DefaultRequestBody(Resource resource)
        {
            this.resource = resource;
        }

        public ISerializedBody Serialize(ITransmissionSettings transmissionSettings)
        {
            if (resource.HasParameters)
            {
                var parameters = resource.GetEncodedParameters();
                var content = Encoding.Default.GetBytes(parameters);

                return new SerializedBody("application/x-www-form-urlencoded", content);
            }

            return new SerializedBody(transmissionSettings.DefaultSerializerContentType, new byte[0]);
        }
    }
}