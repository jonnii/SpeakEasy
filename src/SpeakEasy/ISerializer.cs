using System;
using System.Collections.Generic;
using System.IO;

namespace SpeakEasy
{
    /// <summary>
    /// A serializer is used to turn an object into the body of a http request.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// The media type of this serializer
        /// </summary>
        string MediaType { get; }

        /// <summary>
        /// The content types that this deserializer supports
        /// </summary>
        IEnumerable<string> SupportedMediaTypes { get; }

        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="stream">The stream to write the content into</param>
        /// <param name="body">The object to serialize</param>
        /// <returns>A serialized object</returns>
        void Serialize<T>(Stream stream, T body);

        /// <summary>
        /// Deserializes the body of a response and creates an instance of the given type
        /// </summary>
        /// <param name="body">The body to deserialize</param>
        /// <param name="type">The type to deserialize</param>
        /// <returns>An instance of the given type</returns>
        object Deserialize(Stream body, Type type);

        /// <summary>
        /// Deserializes the body of a response and creates
        /// an instance of the given type
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="body">The body to deserialize</param>
        /// <returns>An instance of type T</returns>
        T Deserialize<T>(Stream body);
    }
}
