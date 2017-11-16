using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// A content represents the body of a request that can be serialized to the request stream
    /// </summary>
    public interface IContent
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
        /// Writes the content to the given stream
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        Task WriteToAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken));
    }
}
