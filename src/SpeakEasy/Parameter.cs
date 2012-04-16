using System;
using System.Linq;

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

        public string ToQueryString()
        {
            var enumerable = Value as Array;

            var value = enumerable != null
                ? string.Join(",", enumerable.Cast<object>().Select(s => s.ToString()))
                : Value.ToString();

            return string.Concat(Name, "=", Uri.EscapeUriString(value));
        }
    }
}