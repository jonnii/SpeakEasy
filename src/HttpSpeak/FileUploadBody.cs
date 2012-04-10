namespace HttpSpeak
{
    public class FileUploadBody : IRequestBody
    {
        private readonly FileUpload[] files;

        public FileUploadBody(FileUpload[] files)
        {
            this.files = files;
        }

        public byte[] SerializeToByteArray(ITransmissionSettings transmissionSettings)
        {
            return new byte[0];
        }
    }
}