using System.IO;

namespace SpeakEasy
{
    public interface IStreamManager
    {
        Stream GetStream(string key);
    }

    public class DefaultStreamManager : IStreamManager
    {
        public Stream GetStream(string key)
        {
            return new MemoryStream();
        }
    }
}
