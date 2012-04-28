using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class HttpRequestSpecification
    {
        [Subject(typeof(HttpRequest))]
        public class in_general : with_request
        {
            It should_allow_auto_redirects = () =>
                request.AllowAutoRedirect.ShouldBeTrue();
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

            protected static TestHttpRequest request;
        }
    }
}