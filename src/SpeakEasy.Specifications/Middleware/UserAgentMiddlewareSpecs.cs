using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Middleware;

namespace SpeakEasy.Specifications.Middleware
{
    [Subject(typeof(UserAgentMiddleware))]
    class UserAgentMiddlewareSpecs : WithFakes
    {
        static UserAgentMiddleware middleware;

        class without_custom_user_agent
        {
            Establish context = () =>
                middleware = new UserAgentMiddleware();

            It should_have_default_speakeasy_useragent = () =>
                middleware.UserAgent.Name.ShouldStartWith("SpeakEasy/");
        }
    }
}
