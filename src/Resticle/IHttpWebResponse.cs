using System;

namespace Resticle
{
    /// <summary>
    /// A simple wrapper around an HttpWebResponse
    /// </summary>
    public interface IHttpWebResponse
    {
        /// <summary>
        /// The uri that responded to the web request
        /// </summary>
        Uri ResponseUri { get; }
    }
}