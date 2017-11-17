using System.IO;

namespace SpeakEasy
{
    internal interface IHttpResponseWithBody : IHttpResponse
    {
        Stream Body { get; }
    }
}
