namespace SpeakEasy
{
    public class UserAgent : IUserAgent
    {
        public static UserAgent SpeakEasy
        {
            get { return new UserAgent("SpeakEasy"); }
        }

        public UserAgent() { }

        public UserAgent(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}