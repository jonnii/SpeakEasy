using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SpeakEasy.Extensions
{
    public static class StreamExtensions
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

        public static Task<Stream> CopyToAsync(this Stream source, Stream target, long bufferSize = 16 * 1024)
        {
            var completionSource = new TaskCompletionSource<Stream>();
            ReadStreamAsyncCore(source, target, bufferSize, completionSource).Iterate(completionSource);
            return completionSource.Task;
        }

        public static Task<Stream> ReadStreamAsync(this Stream source, long bufferSize = 16 * 1024)
        {
            var completionSource = new TaskCompletionSource<Stream>();
            ReadStreamAsyncCore(source, new MemoryStream(), bufferSize, completionSource).Iterate(completionSource);
            return completionSource.Task;
        }

        private static IEnumerable<Task> ReadStreamAsyncCore(Stream responseStream, Stream output, long bufferSize, TaskCompletionSource<Stream> completionSource)
        {
            var buffer = new byte[bufferSize];

            while (true)
            {
                var read = Task<int>.Factory.FromAsync(responseStream.BeginRead, responseStream.EndRead, buffer, 0, buffer.Length, null);
                yield return read;

                if (read.Result == 0)
                {
                    break;
                }
                output.Write(buffer, 0, read.Result);
            }

            output.Seek(0, SeekOrigin.Begin);

            completionSource.TrySetResult(output);
        }
    }
}
