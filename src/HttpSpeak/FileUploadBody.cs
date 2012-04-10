namespace HttpSpeak
{
    public class FileUploadBody : IRequestBody
    {
        private readonly FileUpload[] files;

        public FileUploadBody(FileUpload[] files)
        {
            this.files = files;
        }

        public ISerializedBody Serialize(ITransmissionSettings transmissionSettings)
        {
            throw new System.NotImplementedException();
        }
    }
}