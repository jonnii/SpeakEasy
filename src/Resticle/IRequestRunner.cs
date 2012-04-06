namespace Resticle
{
    /// <summary>
    /// The request runner is responsible for taking a rest request and
    /// running it
    /// </summary>
    public interface IRequestRunner
    {
        /// <summary>
        /// Runs a rest request
        /// </summary>
        /// <param name="request">The request to run</param>
        /// <returns>The rest response</returns>
        IRestResponse Run(IRestRequest request);
    }
}