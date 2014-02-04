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

        public string Name { get; private set; }

        public object Value { get; private set; }

        public bool HasValue
        {
            get { return Value != null; }
        }

        public string ToQueryString(IArrayFormatter arrayFormatter)
        {
            if (!HasValue)
            {
                var message = string.Format(
                    "Could not convert the parameter {0} to a query string because it did not have a value", Name);

                throw new NotSupportedException(message);
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