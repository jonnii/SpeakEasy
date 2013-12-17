namespace SpeakEasy
{
    internal class NullRequestBody : IRequestBody
    {
        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            return new NullSerializableBody(transmissionSettings);
        }
    }
}