using System.Collections.Generic;

namespace HttpSpeak
{
    /// <summary>
    /// Transmission settings covers everything related to serialization and deserialization
    /// </summary>
    public interface ITransmissionSettings
    {
        /// <summary>
        /// The default serializers content type
        /// </summary>
        string DefaultSerializerContentType { get; }

        /// <summary>
        /// The supported deserializable media types supported by
        /// these transmission settings
        /// </summary>
        IEnumerable<string> DeserializableMediaTypes { get; }

        /// <summary>
        /// Serializes an object using the default serializer
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="body">The object to serialize</param>
        /// <returns>A serialized representation of the object</returns>
        string Serialize<T>(T body);

        /// <summary>
        /// Finds a serializer for the given content type
        /// </summary>
        /// <param name="contentType">The content type to deserialize</param>
        /// <returns>A deserializer</returns>
        ISerializer FindSerializer(string contentType);
    }
}