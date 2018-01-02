using System;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// An IHttpResponseState contains all the response state from an http endpoint.
    /// </summary>
    public interface IHttpResponseState : IDisposable
    {
        Uri RequestUrl { get; }

        string ContentType { get; }

        HttpStatusCode StatusCode { get; }

        string ReasonPhrase { get; }

        string ContentEncoding { get; }

        DateTime LastModified { get; }
    }
}
