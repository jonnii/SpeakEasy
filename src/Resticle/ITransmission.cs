namespace Resticle
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransmission
    {
        string ContentType { get; }

        ISerializer DefaultSerializer { get; }
    }
}