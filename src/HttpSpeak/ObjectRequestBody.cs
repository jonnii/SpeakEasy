using System.Text;

namespace HttpSpeak
{
    public class ObjectRequestBody : IRequestBody
    {
        private readonly object body;

        public ObjectRequestBody(object body)
        {
            this.body = body;
        }

        public string ContentType
        {
            get { return string.Empty; }
        }

        public ISerializedBody Serialize(ITransmissionSettings transmissionSettings)
        {
            var serialized = transmissionSettings.Serialize(body);;
            var content = Encoding.Default.GetBytes(serialized);

            return new SerializedBody(transmissionSettings.DefaultSerializerContentType, content);
        }
    }
}