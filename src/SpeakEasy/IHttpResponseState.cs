using System;
using System.Net;

namespace SpeakEasy
{
    /// <summary>
    /// An IHttpResponseState contains all the response state from an http endpoint.
    /// </summary>
    public interface IHttpResponseState
    {
        Uri RequestUrl { get; }

        string Server { get; }

        string ContentType { get; }

        string ContentEncoding { get; }

        string StatusDescription { get; }

        DateTime LastModified { get; }

        HttpStatusCode StatusCode { get; }
    }
}
