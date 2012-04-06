namespace Resticle
{
    /// <summary>
    /// A deserializer is used to turn the body of an http response into an instance
    /// of an object
    /// </summary>
    public interface IDeserializer
    {
        T Deserialize<T>(string body);
    }
}