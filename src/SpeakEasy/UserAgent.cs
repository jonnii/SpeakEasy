using System.Reflection;

namespace SpeakEasy
{
    public class UserAgent : IUserAgent
    {
        public static UserAgent SpeakEasy => new UserAgent(
            $"SpeakEasy/{typeof(UserAgent).GetTypeInfo().Assembly.GetName().Version}");

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
