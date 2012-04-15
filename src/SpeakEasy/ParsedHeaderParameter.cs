namespace SpeakEasy
{
    public class ParsedHeaderParameter
    {
        public ParsedHeaderParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }
    }
}