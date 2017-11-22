using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Middleware
{
    public class UserAgentMiddleware : IHttpMiddleware
    {
        public UserAgentMiddleware()
            : this(SpeakEasy.UserAgent.SpeakEasy)
        {

        }

        public UserAgentMiddleware(IUserAgent userAgent)
        {
            UserAgent = userAgent;
        }

        public IUserAgent UserAgent { get; }

        public IHttpMiddleware Next { get; set; }

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            request.AddHeader(x => x.UserAgent.ParseAdd(UserAgent.Name));

            return await Next.Invoke(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
