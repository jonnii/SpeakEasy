namespace SpeakEasy
{
    public class FileUploadBody : IRequestBody
    {
        private readonly Resource resource;

        private readonly IFile[] files;

        public FileUploadBody(Resource resource, IFile[] files)
        {
            this.resource = resource;
            this.files = files;
        }

        public ISerializableBody Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter)
        {
            return new MultipartMimeDocumentBody(resource, files);
        }
    }
}