using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RequestRunnerSpecification
    {
        [Subject(typeof(RequestRunner))]
        public class when_sending_request : with_request_runner
        {
            Because of = () =>
                Subject.Run(request);

            It should_execute_web_request = () =>
                The<IWebRequestGateway>().WasToldTo(g => g.Send(Param.IsAny<WebRequest>(), Subject.CreateRestResponse));
        }

        [Subject(typeof(RequestRunner))]
        public class when_creating_rest_response : with_request_runner
        {
            Establish context = () =>
            {
                webResponse = An<IHttpWebResponse>();
                webResponse.WhenToldTo(r => r.ResponseUri).Return(new Uri("http://example.com/companies"));
                webResponse.WhenToldTo(r => r.ContentType).Return("application/json");
            };

            Because of = () =>
                response = Subject.CreateRestResponse(webResponse);

            It should_have_response_url_corresponding_to_request = () =>
                response.RequestedUrl.ShouldEqual(new Uri("http://example.com/companies"));

            It should_find_deserializer = () =>
                The<ITransmissionSettings>().WasToldTo(t => t.FindSerializer("application/json"));

            static IHttpWebResponse webResponse;

            static IRestResponse response;
        }

        public class with_request_runner : WithSubject<RequestRunner>
        {
            Establish context = () =>
            {
                request = An<IRestRequest>();
            };

            protected static IRestRequest request;
        }
    }
}