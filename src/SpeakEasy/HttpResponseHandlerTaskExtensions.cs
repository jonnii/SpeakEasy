using System;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public static class HttpResponseHandlerTaskExtensions
    {
        /// <summary>
        /// Deserializes the body of a response as a given type. This
        /// will use the default deserialization settings that are set on the
        /// serializer.
        /// </summary>
        /// <returns>The deserialized body, as an object</returns>
        public static async Task<object> As(this Task<IHttpResponseHandler> httpResponseHandler, Type type)
        {
            return (await httpResponseHandler.ConfigureAwait(false)).As(type);
        }

        /// <summary>
        /// Deserializes the body of a response as a given type. This
        /// will use the default deserialization settings that are set on the
        /// serializer. To override them use an overload of this method.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="httpResponseHandler">The http response handler</param>
        /// <returns>The deserialized body</returns>
        public static async Task<T> As<T>(this Task<IHttpResponseHandler> httpResponseHandler)
        {
            return (await httpResponseHandler.ConfigureAwait(false)).As<T>();
        }

        /// <summary>
        /// Deserializes the body of the response using a function
        /// </summary>
        /// <typeparam name="T">The type of object to create</typeparam>
        /// <param name="httpResponseHandler">The http response handler</param>
        /// <param name="constructor">The constructor function to create the response object with</param>
        /// <returns></returns>
        public static async Task<T> As<T>(this Task<IHttpResponseHandler> httpResponseHandler, Func<IHttpResponseHandler, T> constructor)
        {
            return (await httpResponseHandler.ConfigureAwait(false)).As(constructor);
        }

        /// <summary>
        /// Gets the contents of this http response as a byte array with
        /// the default buffer size
        /// </summary>
        /// <param name="httpResponseHandler">The http response handler</param>
        /// <returns>A byte array with the contents of the response handler</returns>
        public static async Task<byte[]> AsByteArray(this Task<IHttpResponseHandler> httpResponseHandler)
        {
            var handler = await httpResponseHandler.ConfigureAwait(false);
            return await handler.AsByteArray().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the contents of this http response as a byte array.
        /// </summary>
        /// <param name="httpResponseHandler">The http response handler</param>
        /// <param name="bufferSize">The size of the buffer to use when reading the response stream</param>
        /// <returns>A byte array with the contents of the response handler</returns>
        public static async Task<byte[]> AsByteArray(this Task<IHttpResponseHandler> httpResponseHandler, int bufferSize)
        {
            var handler = await httpResponseHandler.ConfigureAwait(false);
            return await handler.AsByteArray(bufferSize).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the contents of this http response as a string
        /// </summary>
        /// <param name="httpResponseHandler">The http response handler</param>
        /// <returns>The string representation of the response</returns>
        public static async Task<string> AsString(this Task<IHttpResponseHandler> httpResponseHandler)
        {
            var handler = await httpResponseHandler.ConfigureAwait(false);
            return await handler.AsString().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the response as an IFile
        /// </summary>
        /// <param name="httpResponseHandler">The http response handler</param>
        /// <returns></returns>
        public static async Task<IFile> AsFile(this Task<IHttpResponseHandler> httpResponseHandler)
        {
            return (await httpResponseHandler.ConfigureAwait(false)).AsFile();
        }
    }
}
