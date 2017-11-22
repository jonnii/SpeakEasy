using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy.Middleware
{
    public class UserAgentMiddleware : IHttpMiddleware
    {
        private readonly IUserAgent userAgent;

        public UserAgentMiddleware()
            : this(UserAgent.SpeakEasy)
        {

        }

        public UserAgentMiddleware(IUserAgent userAgent)
        {
            this.userAgent = userAgent;
        }

        public IHttpMiddleware Next { get; set; }

        public async Task<IHttpResponse> Invoke(IHttpRequest request, CancellationToken cancellationToken)
        {
            request.AddHeader(x => x.UserAgent.ParseAdd(userAgent.Name));

            return await Next.Invoke(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
