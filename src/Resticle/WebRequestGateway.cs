using System;
using System.Net;

namespace Resticle
{
    public class WebRequestGateway : IWebRequestGateway
    {
        public T Send<T>(WebRequest webRequest, Func<IHttpWebResponse, T> responseConverter)
        {
            using (var response = GetResponse(webRequest))
            {
                var responseWrapper = new HttpWebResponseWrapper(response);
                return responseConverter(responseWrapper);
            }
        }

        private HttpWebResponse GetResponse(WebRequest webRequest)
        {
            try
            {
                return (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException wex)
            {
                var innerResponse = wex.Response as HttpWebResponse;
                if (innerResponse != null)
                {
                    return innerResponse;
                }

                throw;
            }
        }
    }
}