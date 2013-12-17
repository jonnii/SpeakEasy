using System;

namespace SpeakEasy
{
    /// <summary>
    /// An array parameter formatter is used to customize the way in which 
    /// array parameters are formatted, for example you may want an array ints such
    /// as this one:
    /// 
    /// new []{1, 2, 3 }
    /// 
    /// to be formatted to the query string like so:
    /// 
    /// ?items=1,2,3
    /// </summary>
    public interface IArrayFormatter
    {
        /// <summary>
        /// Formats a single parameter
        /// </summary>
        string FormatParameter(string name, Array values, Func<object, string> valueFormatter);
    }
}