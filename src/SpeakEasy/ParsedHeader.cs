using System;
using System.Collections.Generic;

namespace SpeakEasy
{
    public class ParsedHeader : Header
    {
        private readonly IDictionary<string, string> parameters;

        public ParsedHeader(string name, string value, IDictionary<string, string> parameters)
            : base(name, value)
        {
            this.parameters = parameters;
        }

        public string GetParameter(string name)
        {
            if (!parameters.ContainsKey(name))
            {
                var message = string.Format("Could not find parameter named {0}", name);

                throw new ArgumentException(message, "name");
            }

            return parameters[name];
        }
    }
}