using System;

namespace Resticle
{
    /// <summary>
    /// A rest request represents one http interaction with
    /// a restful web service
    /// </summary>
    public interface IRestRequest
    {
        /// <summary>
        /// The url that will be requested by this rest request
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// The body of this rest request
        /// </summary>
        object Body { get; }
    }
}