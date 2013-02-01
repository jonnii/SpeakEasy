using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SpeakEasy
{
    internal class HttpWebResponseWrapper : IHttpWebResponse
    {
        private readonly HttpResponseMessage response;

        public HttpWebResponseWrapper(HttpResponseMessage response)
        {
            this.response = response;
        }

        public bool HasContent
        {
            get { return !string.IsNullOrEmpty(ContentType); }
        }

        public string ContentType
        {
            get
            {
                if (response.Content.Headers.ContentType == null)
                {
                    return string.Empty;
                }

                return response.Content.Headers.ContentType.ToString();
            }
        }

        public long ContentLength
        {
            get
            {
                return response.Content.Headers.ContentLength.Value;
            }
        }

        public HttpResponseState BuildState()
        {
            var headers = response.Headers
                .Select(n => new Header(n.Key.ToLowerInvariant(), string.Join(", ", n.Value)))
                .ToArray();

            //response.

            //var cookies = response.Cookies.Cast<System.Net.Cookie>().Select(BuildCookie).ToArray();

            var contentType =
                response.Content.Headers.ContentType == null
                    ? null
                    : response.Content.Headers.ContentType.ToString();

            return new HttpResponseState(
                response.StatusCode,
                "",//response.StatusDescription,
                new Uri("http://example.com"), //response.ResponseUri,
                headers,
                new Cookie[0],
                contentType);
            //response.Server,
            //response.ContentEncoding,
            //response.LastModified);
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