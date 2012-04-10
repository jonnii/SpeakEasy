namespace HttpSpeak
{
    public interface IRequestBody
    {
        byte[] SerializeToByteArray(ITransmissionSettings transmissionSettings);
    }
}