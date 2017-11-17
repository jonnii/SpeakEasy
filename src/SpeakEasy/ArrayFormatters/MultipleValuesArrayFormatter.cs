using System;
using System.Linq;

namespace SpeakEasy.ArrayFormatters
{
    public class MultipleValuesArrayFormatter : IArrayFormatter
    {
        public string FormatParameter(string name, Array values, Func<object, string> valueFormatter)
        {
            var items = values
                .Cast<object>()
                .Select(s => string.Concat(name, "=", valueFormatter(s)));

            return string.Join("&", items);
        }
    }
}
