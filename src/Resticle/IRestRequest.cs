using System.Collections.Generic;
using System.Net;

namespace Resticle
{
    /// <summary>
    /// A rest request represents one http interaction with
    /// a restful web service
    /// </summary>
    public interface IRestRequest
    {
        /// <summary>
        /// The resource that will be requested by this rest request
        /// </summary>
        Resource Resource { get; }

        /// <summary>
        /// The user agent of this rest request
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// The number of headers on this rest request
        /// </summary>
        int NumHeaders { get; }

        /// <summary>
        /// Gets the headers attached to this rest request
        /// </summary>
        IEnumerable<Header> Headers { get; }

        /// <summary>
        /// Creates a web request corresponding to this rest request
        /// </summary>
        /// <param name="transmissionSettings">The current transmissionSettings</param>
        /// <returns>A web request</returns>
        HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings);

        /// <summary>
        /// Adds a header to this rest request
        /// </summary>
        /// <param name="name">The name of the http header</param>
        /// <param name="value">The value of the header</param>
        void AddHeader(string name, string value);
    }
}