using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy.Bodies
{
    internal class NullContent : IContent
    {
        private readonly ITransmissionSettings transmissionSettings;

        public NullContent(ITransmissionSettings transmissionSettings)
        {
            this.transmissionSettings = transmissionSettings;
        }

        public string ContentType
        {
            get { return transmissionSettings.DefaultSerializerContentType; }
        }

        public int ContentLength
        {
            get { return -1; }
        }

        public bool HasContent
        {
            get { return false; }
        }

        public Task WriteToAsync(Stream stream)
        {
            throw new NotSupportedException();
        }
    }
}
