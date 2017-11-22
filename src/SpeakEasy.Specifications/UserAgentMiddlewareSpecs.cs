using Machine.Specifications;
using SpeakEasy.Middleware;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(UserAgentMiddleware))]
    class UserAgentMiddlewareSpecs
    {
        class without_custom_user_agent
        {
        }
    }
}
