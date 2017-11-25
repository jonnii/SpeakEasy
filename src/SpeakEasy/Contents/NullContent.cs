using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Contents
{
    internal class NullContent : IContent
    {
        private readonly ITransmissionSettings transmissionSettings;

        public NullContent(ITransmissionSettings transmissionSettings)
        {
            this.transmissionSettings = transmissionSettings;
        }

        public string ContentType => transmissionSettings.DefaultSerializerContentType;

        public int ContentLength { get; } = -1;

        public bool HasContent { get; } = false;

        public Task WriteToAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotSupportedException();
        }

        public Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }
    }
}
