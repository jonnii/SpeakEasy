namespace Resticle
{
    /// <summary>
    /// Deserialization settings are used to customize the deserializers
    /// </summary>
    public class DeserializationSettings
    {
        public string RootElementPath { get; set; }

        public bool HasRootElementPath
        {
            get { return !string.IsNullOrEmpty(RootElementPath); }
        }
    }
}