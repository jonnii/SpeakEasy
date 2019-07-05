using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SpeakEasy.Serializers
{
    public class DefaultQuerySerializer : IQuerySerializer
    {
        public string FormatParameter(string name, Array values, Func<object, string> valueFormatter)
        {
            var items = string.Join(",", values.Cast<object>()
                .Select(valueFormatter));

            return string.Concat(name, "=", items);
        }

        public IEnumerable<string> FormatParameters(IEnumerable<Parameter> parameters)
        {
            return parameters
                .Where(p => p.HasValue)
                .Select(ToQueryString);
        }

        private string ToQueryString(Parameter parameter)
        {
            if (!parameter.HasValue)
            {
                throw new NotSupportedException($"Could not convert the parameter {parameter.Name} to a query string because it did not have a value");
            }

            if (parameter.Value is Array array)
            {
                return FormatParameter(parameter.Name, array, ToQueryStringValue);
            }

            var value = ToQueryStringValue(parameter.Value);

            return string.Concat(parameter.Name, "=", value);
        }

        private string ToQueryStringValue(object value)
        {
            if (value is DateTime time)
            {
                return time.ToString("o", CultureInfo.InvariantCulture);
            }

            var raw = value.ToString();

            return Uri.EscapeDataString(raw);
        }
    }
}
