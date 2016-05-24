using SpeakEasy.Bodies;

namespace SpeakEasy
{
    internal class NullRequestBody : IRequestBody
    {
        public bool ConsumesResourceParameters
        {
            get { return false; }
        }

        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            return new NullSerializableBody(transmissionSettings);
        }
    }
}
