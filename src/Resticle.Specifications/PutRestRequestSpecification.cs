using System.Net;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class PutRestRequestSpecification
    {
        [Subject(typeof(PutRestRequest))]
        public class when_building_web_request : with_put_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest();

            It should_have_put_method = () =>
                webRequest.Method.ShouldEqual("PUT");

            static WebRequest webRequest;
        }

        public class with_put_request
        {
            Establish context = () =>
                request = new PutRestRequest("http://example.com/companies");

            protected static PutRestRequest request;
        }
    }
}