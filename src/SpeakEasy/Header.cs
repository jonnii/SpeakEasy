using System.Collections.Generic;
using System.Linq;

namespace SpeakEasy
{
    public class Header
    {
        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ParsedHeader Parse()
        {
            var parts = Value.Split(';');

            var parameters = new Dictionary<string, string>();
            foreach (var part in parts.Skip(1))
            {
                var bits = part.Split('=');
                parameters.Add(bits[0].Trim(), bits[1].Trim());
            }

            return new ParsedHeader(
                Name, parts.First(), parameters);
        }
    }
}