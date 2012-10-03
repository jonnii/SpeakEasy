using System.Collections.Generic;
using System.Linq;

namespace SpeakEasy
{
    internal class HeaderParser
    {
        private readonly string value;

        public HeaderParser(string value)
        {
            this.value = value;
        }

        public ParsedHeaderValue Parse()
        {
            var groups = value.Split(',');

            var parameters = new Dictionary<string, ParsedHeaderParameter[]>();
            foreach (var group in groups)
            {
                var parts = group.Split(';');

                var name = parts.First().Trim();

                var groupParameters = parts.Skip(1).Select(s =>
                {
                    var bits = s.Split('=');
                    return new ParsedHeaderParameter(bits[0].Trim(), bits[1].Trim());
                }).ToArray();

                parameters.Add(name, groupParameters);
            }
            return new ParsedHeaderValue(parameters);
        }
    }
}