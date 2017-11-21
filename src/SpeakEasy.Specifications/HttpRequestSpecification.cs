using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;
using SpeakEasy.Specifications.Requests;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpRequest))]
    class HttpRequestSpecification : WithFakes
    {
        static TestHttpRequest request;

        Establish context = () =>
            request = new TestHttpRequest(new Resource("http://example.com/api/companies"));

        class in_general
        {
            It should_allow_auto_redirects = () =>
                request.AllowAutoRedirect.ShouldBeTrue();
        }
    }
}
