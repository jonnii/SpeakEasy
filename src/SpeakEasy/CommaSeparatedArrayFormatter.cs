using System;
using System.Linq;

namespace SpeakEasy
{
    public class CommaSeparatedArrayFormatter : IArrayFormatter
    {
        public string FormatParameter(string name, Array values, Func<object, string> valueFormatter)
        {
            var items = string.Join(",", values.Cast<object>()
                .Select(valueFormatter));

            return string.Concat(name, "=", items);
        }
    }
}