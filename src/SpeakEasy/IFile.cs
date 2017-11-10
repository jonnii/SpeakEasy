using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// A file to upload or a file that has been downloaded
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The file name of the file
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The content type of the file
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Writes the file to a stream asynchronously
        /// </summary>
        /// <param name="stream">The stream to write to</param>
        /// <returns>The writing task</returns>
        Task WriteToAsync(Stream stream);
    }
}
