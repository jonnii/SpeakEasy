using System.Text;

namespace HttpSpeak
{
    public class ObjectBody : IRequestBody
    {
        private readonly object body;

        public ObjectBody(object body)
        {
            this.body = body;
        }

        public byte[] SerializeToByteArray(ITransmissionSettings transmissionSettings)
        {
            var serialized = GetSerializedBody(transmissionSettings);
            return Encoding.Default.GetBytes(serialized);
        }

        private string GetSerializedBody(ITransmissionSettings transmissionSettings)
        {
            return transmissionSettings.Serialize(body);
        }
    }
}