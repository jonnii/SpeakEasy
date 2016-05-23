namespace SpeakEasy
{
    public class ObjectRequestBody : IRequestBody
    {
        private readonly object body;

        public ObjectRequestBody(object body)
        {
            this.body = body;
        }

        public string ContentType { get; } = string.Empty;

        public bool ConsumesResourceParameters { get; } = false;

        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            return new StreamableSerializableBody(
                transmissionSettings.DefaultSerializerContentType,
                stream => transmissionSettings.SerializeAsync(stream, body));
        }
    }
}
