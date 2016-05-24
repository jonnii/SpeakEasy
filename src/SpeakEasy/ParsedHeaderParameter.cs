namespace SpeakEasy
{
    internal class ParsedHeaderParameter
    {
        public ParsedHeaderParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}
