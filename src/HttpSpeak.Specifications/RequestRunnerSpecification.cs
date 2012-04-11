using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class RequestRunnerSpecification
    {
        [Subject(typeof(RequestRunner))]
        public class when_sending_request : with_request_runner
        {
            Because of = () =>
                Subject.Run(request);

            It should_authenticate_request = () =>
                The<IAuthenticator>().WasToldTo(a => a.Authenticate(Param.IsAny<IHttpRequest>()));

            It should_execute_web_request = () =>
                The<IWebRequestGateway>().WasToldTo(g => g.Send(Param.IsAny<WebRequest>(), Subject.CreateHttpResponse));
        }

        [Subject(typeof(RequestRunner))]
        public class when_creating_response : with_request_runner
        {
            Establish context = () =>
            {
                webResponse = An<IHttpWebResponse>();
                webResponse.WhenToldTo(r => r.ResponseUri).Return(new Uri("http://example.com/companies"));
                webResponse.WhenToldTo(r => r.ContentType).Return("application/json");
            };

            Because of = () =>
                response = Subject.CreateHttpResponse(webResponse);

            It should_have_response_url_corresponding_to_request = () =>
                response.RequestedUrl.ShouldEqual(new Uri("http://example.com/companies"));

            It should_find_deserializer = () =>
                The<ITransmissionSettings>().WasToldTo(t => t.FindSerializer("application/json"));

            static IHttpWebResponse webResponse;

            static IHttpResponse response;
        }

        public class with_request_runner : WithSubject<RequestRunner>
        {
            Establish context = () =>
            {
                request = An<IHttpRequest>();
            };

            protected static IHttpRequest request;
        }
    }
}