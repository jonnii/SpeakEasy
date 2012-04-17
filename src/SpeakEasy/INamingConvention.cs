namespace SpeakEasy
{
    /// <summary>
    /// The naming convention is the process by which a segment name is converted into a 
    /// query string parameter
    /// </summary>
    public interface INamingConvention
    {
        /// <summary>
        /// Converts a property name to a parameter name
        /// </summary>
        /// <param name="propertyName">The name of the property to convert</param>
        /// <returns>The converted name</returns>
        string ConvertPropertyNameToParameterName(string propertyName);
    }
}
