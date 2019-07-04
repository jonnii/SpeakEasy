using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SpeakEasy
{
    /// <summary>
    /// An http request represents one http interaction with
    /// a web service that speaks http
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// The resource that will be requested by this http request
        /// </summary>
        Resource Resource { get; }

        /// <summary>
        /// The http method for this request
        /// </summary>
        HttpMethod HttpMethod { get; }

        /// <summary>
        /// The body of this request
        /// </summary>
        IRequestBody Body { get; }

        /// <summary>
        /// Builds the method specific request url
        /// </summary>
        /// <returns>A url</returns>
        string BuildRequestUrl(IParameterFormatter arrayFormatter);

        void AddHeader(string header, string value);

        void AddHeader(Action<HttpRequestHeaders> headers);

        List<Action<HttpRequestHeaders>> Headers { get; }
    }
}
