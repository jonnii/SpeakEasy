using System.Net;
using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Middleware;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Middleware
{
    using SystemHttpClient = System.Net.Http.HttpClient;

    [Subject(typeof(RequestMiddleware))]
    class RequestMiddlewareSpecs : WithFakes
    {
        static RequestMiddleware middleware;

        static IHttpRequest request;

        static HttpRequestMessage web_request;

        Establish context = () =>
        {
            middleware = new RequestMiddleware(new SystemHttpClient(), The<ITransmissionSettings>(), The<IArrayFormatter>(), new CookieContainer());
        };

        class when_building_web_request_with_get_request
        {
            Establish context = () =>
                request = new GetRequest(Resource.Create("http://example.com/companies"));

            Because of = () =>
                web_request = middleware.BuildHttpRequestMessage(request);

            It should_set_url = () =>
                web_request.RequestUri.ToString().ShouldEqual("http://example.com/companies");

            It should_set_method = () =>
                web_request.Method.ShouldEqual(HttpMethod.Get);
        }

        class when_building_web_request_with_options_request
        {
            Establish context = () =>
                request = new OptionsRequest(Resource.Create("http://example.com/companies"));

            Because of = () =>
                web_request = middleware.BuildHttpRequestMessage(request);

            It should_set_method = () =>
                web_request.Method.ShouldEqual(HttpMethod.Options);
        }

        //class when_creating_response
        //{
        //    Establish context = () =>
        //    {
        //        webResponse = An<IHttpWebResponse>();
        //        webResponse.WhenToldTo(r => r.ContentType).Return("application/json");
        //    };

        //    Because of = () =>
        //        Subject.CreateHttpResponse(webResponse, new MemoryStream());

        //    It should_find_deserializer = () =>
        //        The<ITransmissionSettings>().WasToldTo(t => t.FindSerializer("application/json"));

        //    static IHttpWebResponse webResponse;
        //}
    }
}
