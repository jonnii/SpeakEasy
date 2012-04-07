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
        /// Creates a web request corresponding to this rest request
        /// </summary>
        /// <param name="transmissionSettings">The current transmissionSettings</param>
        /// <returns>A web request</returns>
        HttpWebRequest BuildWebRequest(ITransmissionSettings transmissionSettings);
    }
}