// using System.IO;
// using System.Linq;
// using System.Net;

// namespace SpeakEasy
// {
//     internal class HttpWebResponseWrapper : IHttpWebResponse
//     {
//         private readonly HttpWebResponse response;

//         public HttpWebResponseWrapper(HttpWebResponse response)
//         {
//             this.response = response;
//         }

//         public bool HasContent => !string.IsNullOrEmpty(ContentType);

//         public string ContentType => response.ContentType;

//         public long ContentLength => response.ContentLength;

//         public Stream GetResponseStream()
//         {
//             return response.GetResponseStream();
//         }

//         public HttpResponseState BuildState()
//         {
//             var headerNames = response.Headers.AllKeys;
//             var headers = headerNames.Select(n => new Header(n.ToLowerInvariant(), response.Headers[n])).ToArray();

//             var cookies = response.Cookies.Cast<System.Net.Cookie>().Select(BuildCookie).ToArray();

//             return new HttpResponseState(
//                 response.StatusCode,
//                 response.StatusDescription,
//                 response.ResponseUri,
//                 headers,
//                 cookies,
//                 response.ContentType,
//                 response.Server,
//                 response.ContentEncoding,
//                 response.LastModified);
//         }

//         private Cookie BuildCookie(System.Net.Cookie cookie)
//         {
//             return new Cookie(
//                 cookie.Comment,
//                 cookie.CommentUri,
//                 cookie.Discard,
//                 cookie.Domain,
//                 cookie.Expired,
//                 cookie.Expires,
//                 cookie.HttpOnly,
//                 cookie.Name,
//                 cookie.Path,
//                 cookie.Port,
//                 cookie.Secure,
//                 cookie.TimeStamp,
//                 cookie.Value,
//                 cookie.Version);
//         }

//         public void Dispose()
//         {
//             response.Close();
//         }
//     }
// }
