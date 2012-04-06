using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestRequestSpecification
    {
        [Subject(typeof(RestRequest))]
        public class when_building_web_request : with_rest_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmission);

            It should_set_content_type = () =>
                webRequest.ContentType.ShouldEqual("text/xml");

            static HttpWebRequest webRequest;
        }

        public class with_rest_request : WithFakes
        {
            Establish context = () =>
            {
                transmission = An<ITransmission>();
                transmission.WhenToldTo(s => s.ContentType).Return("text/xml");

                webRequestGateway = An<IWebRequestGateway>();

                request = new TestRestRequest("http://example.com/api/companies");
            };

            protected static TestRestRequest request;

            protected static ITransmission transmission;

            protected static IWebRequestGateway webRequestGateway;
        }
    }

    public class TestRestRequest : RestRequest
    {
        public TestRestRequest(string url)
            : base(url) { }
    }
}
