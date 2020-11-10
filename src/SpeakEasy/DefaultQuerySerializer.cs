using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SpeakEasy
{
    public class DefaultQuerySerializer : IQuerySerializer
    {
        public bool ExpandArrayValues { get; set; } = true;

        public IEnumerable<string> Serialize(IEnumerable<Parameter> parameters)
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
                if (ExpandArrayValues)
                {
                    foreach (var item in array)
                    {
                        yield return string.Concat(parameter.Name, "=", ToQueryStringValue(item));
                    }
                }
                else
                {
                    var items = string.Join(",", array.Cast<object>()
                        .Select(ToQueryStringValue));

                    yield return string.Concat(parameter.Name, "=", items);
                }
            }
            else
            {
                var value = ToQueryStringValue(parameter.Value);

                yield return string.Concat(parameter.Name, "=", value);
            }
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
