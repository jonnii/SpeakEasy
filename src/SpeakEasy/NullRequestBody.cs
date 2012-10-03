namespace SpeakEasy
{
    internal class NullRequestBody : IRequestBody
    {
        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings)
        {
            return new NullSerializableBody(transmissionSettings);
        }
    }
}