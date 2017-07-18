using System;

namespace SpeakEasy
{
    /// <summary>
    /// A response handler exposes a response matching a specific http status code.
    /// </summary>
    public interface IHttpResponseHandler
    {
        /// <summary>
        /// The response that created this response handler
        /// </summary>
        IHttpResponse Response { get; }

        /// <summary>
        /// Deserializes the body of a response as a given type.
        /// </summary>
        /// <returns>The deserialized body, as an object</returns>
        object As(Type type);

        /// <summary>
        /// Deserializes the body of a response as a given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <returns>The deserialized body</returns>
        T As<T>();

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