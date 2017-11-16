using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Specifications.Requests;

namespace SpeakEasy.Specifications
{
    public class HttpRequestSpecification
    {
        [Subject(typeof(HttpRequest))]
        public class in_general : with_request
        {
            It should_allow_auto_redirects = () =>
                request.AllowAutoRedirect.ShouldBeTrue();

            It should_not_have_custom_user_agent = () =>
                request.HasUserAgent.ShouldBeFalse();
        }

        [Subject(typeof(HttpRequest))]
        public class when_custom_user_agent_defined : with_request
        {
            Establish context = () =>
                request.UserAgent = new UserAgent("awesome agent!");

            It should_have_custom_user_agent = () =>
                request.HasUserAgent.ShouldBeTrue();
        }

        [Subject(typeof(HttpRequest))]
        public class when_adding_header : with_request
        {
            Because of = () =>
                request.AddHeader("header", "value");

            It should_add_header = () =>
                request.NumHeaders.ShouldEqual(1);
        }

        public class with_request : WithFakes
        {
            Establish context = () =>
                request = new TestHttpRequest(new Resource("http://example.com/api/companies"));

            internal static TestHttpRequest request;
        }
    }
}