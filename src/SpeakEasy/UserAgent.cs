namespace SpeakEasy
{
    public class UserAgent : IUserAgent
    {
        public static UserAgent SpeakEasy => new UserAgent("SpeakEasy");

        public UserAgent()
        {
        }

        public UserAgent(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
