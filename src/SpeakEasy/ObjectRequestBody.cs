using System.Text;

namespace SpeakEasy
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

        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings)
        {
            var serialized = transmissionSettings.Serialize(body);
            var content = Encoding.UTF8.GetBytes(serialized);

            return new SerializableByteArray(transmissionSettings.DefaultSerializerContentType, content);
        }
    }
}