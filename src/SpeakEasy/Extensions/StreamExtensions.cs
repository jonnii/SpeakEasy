using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy.Extensions
{
    internal static class StreamExtensions
    {
        public static async Task<byte[]> ReadAsByteArray(this Stream input, int bufferSize = 16 * 1024)
        {
            var buffer = new byte[bufferSize];

            using (var memoryStream = new MemoryStream())
            {
                int read;

                while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
