namespace Resticle
{
    /// <summary>
    /// The rest request dispatcher is responsible for dispatching rest requests and 
    /// creating a corresponding rest response
    /// </summary>
    public interface IRestRequestDispatcher
    {
        /// <summary>
        /// Dispatches a rest request
        /// </summary>
        /// <param name="request">The request to dispatch</param>
        /// <returns>A rest response</returns>
        IRestResponse Dispatch(IRestRequest request);
    }
}