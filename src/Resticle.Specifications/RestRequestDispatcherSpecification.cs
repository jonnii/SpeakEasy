using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestRequestDispatcherSpecification
    {
        [Subject(typeof(RestRequestDispatcher))]
        public class when_dispatching_request : WithSubject<RestRequestDispatcher>
        {
            Establish context = () =>
            {
                request = An<IRestRequest>();
                request.WhenToldTo(r => r.Url).Return(new Uri("http://example.com/companies"));

                The<IWebRequestGateway>().WhenToldTo(g => g.Send(Param.IsAny<WebRequest>(), Subject.CreateRestResponse));
            };

            Because of = () =>
                Subject.Dispatch(request);

            It should_build_http_request = () =>
                request.WasToldTo(r => r.BuildWebRequest());

            It should_execute_web_request = () =>
                The<IWebRequestGateway>().WasToldTo(g => g.Send(Param.IsAny<WebRequest>(), Subject.CreateRestResponse));

            static IRestRequest request;
        }

        [Subject(typeof(RestRequestDispatcher))]
        public class when_building_rest_response : WithSubject<RestRequestDispatcher>
        {
            Establish context = () =>
            {
                webResponse = An<IHttpWebResponse>();
                webResponse.WhenToldTo(r => r.ResponseUri).Return(new Uri("http://example.com/companies"));
            };

            Because of = () =>
                response = Subject.CreateRestResponse(webResponse);

            It should_have_response_url_corresponding_to_request = () =>
                response.RequestedUrl.ShouldEqual(new Uri("http://example.com/companies"));

            static IHttpWebResponse webResponse;

            static RestResponse response;
        }
    }
}
