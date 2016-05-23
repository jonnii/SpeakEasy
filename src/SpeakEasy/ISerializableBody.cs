using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// A serializable body represents the body of a request that can be serialized to the request stream
    /// </summary>
    public interface ISerializableBody
    {
        /// <summary>
        /// The content type of the body
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// The length of the content
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// Indicates whether or not this body has content
        /// </summary>
        bool HasContent { get; }

        /// <summary>
        /// Writes the body to the given stream
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        Task WriteToAsync(Stream stream);
    }
}
