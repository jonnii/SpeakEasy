namespace SpeakEasy
{
    public class Header
    {
        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        internal ParsedHeaderValue ParseValue()
        {
            var parser = new HeaderParser(Value);
            return parser.Parse();
        }
    }
}
