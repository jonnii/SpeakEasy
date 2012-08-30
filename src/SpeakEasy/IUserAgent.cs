namespace SpeakEasy
{
    /// <summary>
    /// A user agent represents the identity of client that is making
    /// a web request, for example a browser.
    /// </summary>
    public interface IUserAgent
    {
        /// <summary>
        /// The name of this user agent
        /// </summary>
        string Name { get; }
    }
}