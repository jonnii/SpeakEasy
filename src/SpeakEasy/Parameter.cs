using System;
using System.Globalization;

namespace SpeakEasy
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; }

        public bool HasValue => Value != null;

        public string ToQueryString(IArrayFormatter arrayFormatter)
        {
            if (!HasValue)
            {
                throw new NotSupportedException($"Could not convert the parameter {Name} to a query string because it did not have a value");
            }

            var enumerable = Value as Array;

            if (enumerable != null)
            {
                return arrayFormatter.FormatParameter(Name, enumerable, ToQueryStringValue);
            }

            var value = ToQueryStringValue(Value);

            return string.Concat(Name, "=", value);
        }

        private string ToQueryStringValue(object value)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToString("o", CultureInfo.InvariantCulture);
            }

            var raw = value.ToString();

            return Uri.EscapeUriString(raw);
        }
    }
}
