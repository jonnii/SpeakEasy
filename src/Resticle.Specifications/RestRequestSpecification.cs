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
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_set_content_type = () =>
                webRequest.ContentType.ShouldEqual("text/xml");

            It should_set_supported_accept_types = () =>
                webRequest.Accept.ShouldEqual("text/xml, application/json");

            static HttpWebRequest webRequest;
        }

        public class with_rest_request : WithFakes
        {
            Establish context = () =>
            {
                transmissionSettings = An<ITransmissionSettings>();
                transmissionSettings.WhenToldTo(t => t.DefaultSerializerContentType).Return("text/xml");
                transmissionSettings.WhenToldTo(t => t.DeserializableMediaTypes).Return(new[] { "text/xml", "application/json" });

                webRequestGateway = An<IWebRequestGateway>();

                request = new TestRestRequest(new Resource("http://example.com/api/companies"));
            };

            protected static TestRestRequest request;

            protected static ITransmissionSettings transmissionSettings;

            protected static IWebRequestGateway webRequestGateway;
        }
    }

    public class TestRestRequest : GetLikeRestRequest
    {
        public TestRestRequest(Resource resource)
            : base(resource) { }
    }
}
