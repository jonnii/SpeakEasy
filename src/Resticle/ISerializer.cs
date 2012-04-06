namespace Resticle
{
    /// <summary>
    /// A serializer is used to turn an object into the body of a rest request.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// The content type of this serializer
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="t">The object to serialize</param>
        /// <returns>A serialized object</returns>
        string Serialize<T>(T t);
    }
}