using System.IO;

namespace SpeakEasy.Extensions
{
    internal static class StreamExtensions
    {
        public static byte[] ReadAsByteArray(this Stream input, int bufferSize = 16 * 1024)
        {
            var buffer = new byte[bufferSize];
            using (var memoryStream = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
