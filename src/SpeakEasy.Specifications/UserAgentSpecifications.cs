using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(UserAgent))]
    class UserAgentSpecifications
    {
        class default_user_agent
        {
            It should_contain_app_version = () =>
                UserAgent.SpeakEasy.Name.ShouldEqual("SpeakEasy/1.0.0.0");
        }
    }
}
