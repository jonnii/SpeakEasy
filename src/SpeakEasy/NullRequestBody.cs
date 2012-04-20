namespace SpeakEasy
{
    public class NullRequestBody : IRequestBody
    {
        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings)
        {
            return new NullSerializableBody(transmissionSettings);
        }
    }
}