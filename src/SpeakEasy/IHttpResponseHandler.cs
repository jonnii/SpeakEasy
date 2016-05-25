using System;

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
        /// serializer.
        /// </summary>
        /// <returns>The deserialized body, as an object</returns>
        object As(Type type);

        /// <summary>
        /// Deserializes the body of a response as a given type.
        /// </summary>
        /// <param name="type">The type of the object to deserialize</param>
        /// <param name="deserializationSettings">The settings used to deserialize the contents</param>
        /// <returns>The deserialized body, as an object</returns>
        object As(Type type, DeserializationSettings deserializationSettings);

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
        /// Deserializes the body of the response using a function
        /// </summary>
        /// <typeparam name="T">The type of object to create</typeparam>
        /// <param name="constructor">The constructor function to create the response object with</param>
        /// <returns></returns>
        T As<T>(Func<IHttpResponseHandler, T> constructor);

        /// <summary>
        /// Gets the contents of this http response as a byte array with
        /// the default buffer size
        /// </summary>
        /// <returns>A byte array with the contents of the response handler</returns>
        byte[] AsByteArray();

        /// <summary>
        /// Gets the contents of this http response as a byte array.
        /// </summary>
        /// <param name="bufferSize">The size of the buffer to use when reading the response stream</param>
        /// <returns>A byte array with the contents of the response handler</returns>
        byte[] AsByteArray(int bufferSize);

        /// <summary>
        /// Gets the contents of this http response as a string
        /// </summary>
        /// <returns>The string representation of the response</returns>
        string AsString();

        /// <summary>
        /// Gets the response as an IFile
        /// </summary>
        /// <returns></returns>
        IFile AsFile();
    }
}
