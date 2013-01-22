using System.Linq;

namespace SpeakEasy
{
    internal partial class HttpWebResponseWrapper
    {
        public HttpResponseState BuildState()
        {
            var headerNames = response.Headers.AllKeys;
            var headers = headerNames.Select(n => new Header(n.ToLowerInvariant(), response.Headers[n])).ToArray();

            var cookies = response.Cookies.Cast<System.Net.Cookie>().Select(BuildCookie).ToArray();

            return new HttpResponseState(
                response.StatusCode,
                response.StatusDescription,
                response.ResponseUri,
                headers,
                cookies,
                response.ContentType);
            //response.Server,
            //response.ContentEncoding,
            //response.LastModified);
        }
    }
}
