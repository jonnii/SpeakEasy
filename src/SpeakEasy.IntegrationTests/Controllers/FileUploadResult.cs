//using System.Net.Http.Headers;

namespace SpeakEasy.IntegrationTests.Controllers
{
    public class FileUploadResult
    {
        public TextFileInfo[] TextFileInfos { get; set; } = new TextFileInfo[0];

        public Parameter[] Parameters { get; set; } = new Parameter[0];
    }
}
