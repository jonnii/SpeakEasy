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
        /// Serializes the request body
        /// </summary>
        /// <param name="transmissionSettings">The transmission settings</param>
        /// <param name="arrayFormatter">The array formatter for array parameters</param>
        /// <returns>A byte array with the contents of this body</returns>
        ISerializableBody Serialize(ITransmissionSettings transmissionSettings, IArrayFormatter arrayFormatter);
    }
}