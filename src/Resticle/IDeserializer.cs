using System.Collections.Generic;

namespace Resticle
{
    /// <summary>
    /// A deserializer is used to turn the body of an http response into an instance
    /// of an object
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// The content types that this deserializer supports
        /// </summary>
        IEnumerable<string> SupportedMediaTypes { get; }

        /// <summary>
        /// Deserializes the body of a rest response and creates
        /// an instance of the given type
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="body">The body to deserialize</param>
        /// <returns>An instance of type T</returns>
        T Deserialize<T>(string body);
    }
}