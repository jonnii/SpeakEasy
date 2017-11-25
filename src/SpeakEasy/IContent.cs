using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    /// <summary>
    /// A content represents the body of a request that can be serialized to the request stream
    /// </summary>
    public interface IContent
    {
        Task WriteTo(HttpRequestMessage httpRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}
