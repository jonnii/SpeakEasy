using SpeakEasy.Contents;

namespace SpeakEasy.Bodies
{
    internal class FileUploadBody : IRequestBody
    {
        private readonly Resource resource;

        private readonly IFile[] files;

        public FileUploadBody(Resource resource, IFile[] files)
        {
            this.resource = resource;
            this.files = files;
        }

        public bool ConsumesResourceParameters { get; } = true;

        public IContent Serialize(ITransmissionSettings transmissionSettings, IParameterFormatter arrayFormatter)
        {
            return new MultipartMimeContent(resource, files);
        }
    }
}
