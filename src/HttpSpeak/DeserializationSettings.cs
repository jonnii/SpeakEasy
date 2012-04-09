namespace HttpSpeak
{
    /// <summary>
    /// Deserialization settings are used to customize the deserializers
    /// </summary>
    public class DeserializationSettings
    {
        /// <summary>
        /// The RootElementPath is the path to the root element in the response
        /// </summary>
        public string RootElementPath { get; set; }

        /// <summary>
        /// Skips the root element, because often responses are contained in a
        /// response object
        /// </summary>
        public bool SkipRootElement { get; set; }

        /// <summary>
        /// Indicates whether or not the deserialization settings has
        /// the root element path property set
        /// </summary>
        public bool HasRootElementPath
        {
            get { return !string.IsNullOrEmpty(RootElementPath); }
        }
    }
}