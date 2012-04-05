namespace Resticle
{
    /// <summary>
    /// A rest request builder is responsible for building a rest request that can
    /// then be executed
    /// </summary>
    public interface IRestRequestBuilder
    {
        /// <summary>
        /// Builds the rest request
        /// </summary>
        /// <returns>A new rest request which can be executed</returns>
        IRestRequest Build();
    }
}