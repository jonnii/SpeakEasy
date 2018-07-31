using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(UserAgent))]
    class UserAgentSpecs
    {
        class default_user_agent
        {
            It should_contain_app_version = () =>
                UserAgent.SpeakEasy.Name.ShouldContain("SpeakEasy/1.");
        }
    }
}
