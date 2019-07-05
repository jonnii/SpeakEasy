using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SpeakEasy.Serializers
{
    public class DefaultQuerySerializer : IQuerySerializer
    {
        public bool ExpandArrayValues { get; set; } = true;

        public string FormatParameter(string name, Array values, Func<object, string> valueFormatter)
        {
            var items = string.Join(",", values.Cast<object>()
                .Select(valueFormatter));

            return string.Concat(name, "=", items);
        }

//        public string FormatExpanded(string name, Array values, Func<object, string> valueFormatter)
//        {
//            var items = values
//                .Cast<object>()
//                .Select(s => string.Concat(name, "=", valueFormatter(s)));
//
//            return string.Join("&", items);
//        }

        public IEnumerable<string> FormatParameters(IEnumerable<Parameter> parameters)
        {
            return parameters
                .Where(p => p.HasValue)
                .SelectMany(ToQueryString);
        }

        private IEnumerable<string> ToQueryString(Parameter parameter)
        {
            if (!parameter.HasValue)
            {
                throw new NotSupportedException($"Could not convert the parameter {parameter.Name} to a query string because it did not have a value");
            }

            if (parameter.Value is Array array)
            {
                if (!ExpandArrayValues)
                {
                    foreach (var t in array)
                    {
                        yield return string.Concat(parameter.Name, "=", ToQueryStringValue(t));

//                        yield return FormatParameter(parameter.Name, t, ToQueryStringValue);
                    }
                }
                else
                {
                    var items = string.Join(",", array.Cast<object>()
                        .Select(ToQueryStringValue));

                    yield return string.Concat(parameter.Name, "=", items);
                }
            }

            var value = ToQueryStringValue(parameter.Value);

            yield return string.Concat(parameter.Name, "=", value);
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
