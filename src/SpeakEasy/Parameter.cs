using System;

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
            return string.Concat(Name, "=", Uri.EscapeUriString(Value.ToString()));
        }
    }
}