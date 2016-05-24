using SpeakEasy.Bodies;

namespace SpeakEasy
{
    internal class NullRequestBody : IRequestBody
    {
        public bool ConsumesResourceParameters
        {
            get { return false; }
        }

        public IContent Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            return new NullContent(transmissionSettings);
        }
    }
}
