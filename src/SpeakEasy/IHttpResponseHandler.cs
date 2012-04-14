namespace SpeakEasy
{
    /// <summary>
    /// A response handler exposes a response matching a specific http status code.
    /// </summary>
    public interface IHttpResponseHandler
    {
        /// <summary>
        /// Deserializes the body of a response as a given type. This 
        /// will use the default deserialization settings that are set on the 
        /// serializer. To override them use an overload of this method.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <returns>The deserialized body</returns>
        T As<T>();

        /// <summary>
        /// Deserializes the body of a response as a given type.
        /// </summary>
        /// <param name="deserializationSettings">The settings used to deserialize the contents</param>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <returns>The deserialized body</returns>
        T As<T>(DeserializationSettings deserializationSettings);

        /// <summary>
        /// Gets the contents of this http response as a byte array.
        /// </summary>
        /// <returns>A byte array with the contents of the response handler</returns>
        byte[] AsByteArray();

        /// <summary>
        /// Gets the contents of this http response as a string
        /// </summary>
        /// <returns>The string representation of the response</returns>
        string AsString();
    }
}