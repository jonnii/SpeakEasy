using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RequestContextSpecification
    {
        [Subject(typeof(RestRequest))]
        public class when_sending_request : with_request_context
        {
            Because of = () =>
                requestContext.Send(webRequestGateway);

            It should_execute_web_request = () =>
                webRequestGateway.WasToldTo(g => g.Send(Param.IsAny<WebRequest>(), requestContext.CreateRestResponse));
        }

        [Subject(typeof(RestRequest))]
        public class when_building_rest_response : with_request_context
        {
            Establish context = () =>
            {
                webResponse = An<IHttpWebResponse>();
                webResponse.WhenToldTo(r => r.ResponseUri).Return(new Uri("http://example.com/companies"));
            };

            Because of = () =>
                response = requestContext.CreateRestResponse(webResponse);

            It should_have_response_url_corresponding_to_request = () =>
                response.RequestedUrl.ShouldEqual(new Uri("http://example.com/companies"));

            static IHttpWebResponse webResponse;

            static RestResponse response;
        }

        public class with_request_context : WithFakes
        {
            Establish context = () =>
            {
                transmission = An<ITransmission>();
                request = An<IRestRequest>();
                webRequestGateway = An<IWebRequestGateway>();

                requestContext = new RequestContext(transmission, request);
            };

            protected static IWebRequestGateway webRequestGateway;

            protected static ITransmission transmission;

            protected static IRestRequest request;

            protected static RequestContext requestContext;
        }
    }
}