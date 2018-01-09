using SpeakEasy.Contents;

namespace SpeakEasy.Bodies
{
    internal class ObjectRequestBody : IRequestBody
    {
        private readonly object body;

        public ObjectRequestBody(object body)
        {
            this.body = body;
        }

        public string ContentType { get; } = string.Empty;

        public bool ConsumesResourceParameters { get; } = false;

        public IContent Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            return new StreamableContent(
                transmissionSettings.DefaultSerializerContentType,
                (stream, cancellationToken) => transmissionSettings.SerializeAsync(stream, body, cancellationToken));
        }
    }
}
