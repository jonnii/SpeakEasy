namespace Resticle
{
    /// <summary>
    /// A rest response handler exposes a rest response matching a specific http status code.
    /// </summary>
    public interface IRestResponseHandler
    {
        /// <summary>
        /// Deserializes the body of a rest response as a given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <returns>The deserialized body</returns>
        T Unwrap<T>();
    }
}