using System.Collections.Generic;

namespace SpeakEasy
{
    /// <summary>
    /// A query formatter is used to customize the way in which query parameters
    /// are formatted to the url.
    /// </summary>
    public interface IQuerySerializer
    {
        IEnumerable<string> Serialize(IEnumerable<Parameter> parameters);
    }
}
