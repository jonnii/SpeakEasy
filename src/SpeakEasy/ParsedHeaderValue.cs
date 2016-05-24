using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeakEasy
{
    internal class ParsedHeaderValue
    {
        private readonly IDictionary<string, ParsedHeaderParameter[]> parameters;

        public ParsedHeaderValue(IDictionary<string, ParsedHeaderParameter[]> parameters)
        {
            this.parameters = parameters;
        }

        public IEnumerable<string> Keys => parameters.Keys;

        public string GetParameter(string key, string name)
        {
            if (!parameters.ContainsKey(key))
            {
                throw new ArgumentException($"Could not find parameter named {key}", nameof(key));
            }

            return GetParameters(key).Single(p => p.Name == name).Value;
        }

        public IEnumerable<ParsedHeaderParameter> GetParameters(string key)
        {
            return parameters[key];
        }
    }
}
