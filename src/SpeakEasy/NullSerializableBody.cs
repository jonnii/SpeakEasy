using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class NullSerializableBody : ISerializableBody
    {
        private readonly ITransmissionSettings transmissionSettings;

        public NullSerializableBody(ITransmissionSettings transmissionSettings)
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

        public Task WriteTo(Stream stream)
        {
            throw new NotSupportedException();
        }
    }
}