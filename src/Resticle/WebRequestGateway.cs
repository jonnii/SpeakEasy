using System;
using System.Net;

namespace Resticle
{
    public class WebRequestGateway : IWebRequestGateway
    {
        public T Send<T>(WebRequest webRequest, Func<IHttpWebResponse, T> responseConverter)
        {
            using (var response = (HttpWebResponse)webRequest.GetResponse())
            {
                var responseWrapper = new HttpWebResponseWrapper(response);
                return responseConverter(responseWrapper);
            }
        }
    }
}