using System;
using System.Net;

namespace Resticle
{
    /// <summary>
    /// A simple wrapper around an HttpWebResponse
    /// </summary>
    public interface IHttpWebResponse
    {
        Uri ResponseUri { get; }

        HttpStatusCode StatusCode { get; }
    }
}