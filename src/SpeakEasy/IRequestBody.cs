namespace SpeakEasy
{
    /// <summary>
    /// A request body contains all the details of the body of an http request, for example if you are posting an object
    /// then the request body will the object you are posting, while if you are uploading a file then the request body
    /// will be a collection of files. Request bodies need to be serialized before they can attached to a request
    /// </summary>
    public interface IRequestBody
    {
        /// <summary>
        /// Indicates whether or not this request body will use the resource parameters when serializing
        /// the request body. This is used to indicate whether or not the request can use the resource
        /// parameters for something else (namely query parameters).
        /// </summary>
        bool ConsumesResourceParameters { get; }

        /// <summary>
        /// Serializes the request body
        /// </summary>
        /// <param name="transmissionSettings">The transmission settings</param>
        /// <param name="arrayFormatter">The array formatter for array parameters</param>
        /// <returns>A byte array with the contents of this body</returns>
        IContent Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter);
    }
}
