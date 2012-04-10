namespace HttpSpeak
{
    public interface IRequestBody
    {
        /// <summary>
        /// Serializes the request body
        /// </summary>
        /// <param name="transmissionSettings">The transmission settings</param>
        /// <returns>A byte array with the contents of this body</returns>
        ISerializableBody Serialize(ITransmissionSettings transmissionSettings);
    }
}