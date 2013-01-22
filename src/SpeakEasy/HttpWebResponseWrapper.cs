using System.IO;
using System.Net;

namespace SpeakEasy
{
    internal partial class HttpWebResponseWrapper : IHttpWebResponse
    {
        private readonly HttpWebResponse response;

        public HttpWebResponseWrapper(HttpWebResponse response)
        {
            this.response = response;
        }

        public bool HasContent
        {
            get { return !string.IsNullOrEmpty(ContentType); }
        }

        public string ContentType
        {
            get { return response.ContentType; }
        }

        public long ContentLength
        {
            get { return response.ContentLength; }
        }

        public Stream GetResponseStream()
        {
            return response.GetResponseStream();
        }

        private Cookie BuildCookie(System.Net.Cookie cookie)
        {
            return new Cookie(
                cookie.Comment,
                cookie.CommentUri,
                cookie.Discard,
                cookie.Domain,
                cookie.Expired,
                cookie.Expires,
                cookie.HttpOnly,
                cookie.Name,
                cookie.Path,
                cookie.Port,
                cookie.Secure,
                cookie.TimeStamp,
                cookie.Value,
                cookie.Version);
        }

        public void Dispose()
        {
            response.Dispose();
        }
    }
}