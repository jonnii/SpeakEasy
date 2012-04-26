using System;
using System.IO;
using System.Linq;
using System.Net;

namespace SpeakEasy
{
    public class HttpWebResponseWrapper : IHttpWebResponse
    {
        private readonly HttpWebResponse response;

        public HttpWebResponseWrapper(HttpWebResponse response)
        {
            this.response = response;
        }

        public Uri ResponseUri
        {
            get { return response.ResponseUri; }
        }

        public HttpStatusCode StatusCode
        {
            get { return response.StatusCode; }
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

        public Header[] Headers
        {
            get
            {
                var headerNames = response.Headers.AllKeys;
                return headerNames.Select(n => new Header(n.ToLowerInvariant(), response.Headers[n])).ToArray();
            }
        }

        public Cookie[] Cookies
        {
            get
            {
                return response.Cookies.Cast<System.Net.Cookie>().Select(BuildCookie).ToArray();
            }
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

        public Stream GetResponseStream()
        {
            return response.GetResponseStream();
        }

        public void Dispose()
        {
            response.Close();
        }
    }
}