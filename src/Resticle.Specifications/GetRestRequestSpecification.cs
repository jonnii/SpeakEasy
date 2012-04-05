using System;
using System.Net;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class GetRestRequestSpecification
    {
        [Subject(typeof(GetRestRequest))]
        public class when_building_web_request : with_get_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest();

            It should_set_url = () =>
                request.Url.ShouldEqual(new Uri("http://example.com/companies"));

            It should_set_request_to_get_request = () =>
                webRequest.Method.ShouldEqual("GET");

            static WebRequest webRequest;
        }

        public class with_get_request
        {
            Establish context = () =>
                request = new GetRestRequest(new Uri("http://example.com/companies"));

            protected static GetRestRequest request;
        }
    }
}
