using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(RequestRunner))]
    class RequestRunnerSpecification : WithSubject<RequestRunner>
    {
        static HttpRequestMessage webRequest;

        static IHttpRequest request;

        Establish context = () =>
        {
            request = An<IHttpRequest>();
            request.WhenToldTo(r => r.BuildRequestUrl(Param.IsAny<IArrayFormatter>())).Return("http://example.com");
            request.WhenToldTo(r => r.HttpMethod).Return(HttpMethod.Get);
        };

        class when_building_web_request_with_get_request
        {
            Establish context = () =>
                request = new GetRequest(new Resource("http://example.com/companies"));

            Because of = () =>
                webRequest = Subject.BuildHttpRequestMessage(request);

            It should_set_url = () =>
                webRequest.RequestUri.ToString().ShouldEqual("http://example.com/companies");

            It should_set_method = () =>
                webRequest.Method.ShouldEqual(HttpMethod.Get);

            // It should_initialize_cookie_container = () =>
            //     webRequest.CookieContainer.ShouldNotBeNull();

            // It should_get_cookie_from_cookie_strategy = () =>
            //     The<ICookieStrategy>().WasToldTo(s => s.Get(Param.IsAny<IHttpRequest>()));
        }


        class when_building_web_request_with_options_request
        {
            Establish context = () =>
                request = new OptionsRequest(new Resource("http://example.com/companies"));

            Because of = () =>
                webRequest = Subject.BuildHttpRequestMessage(request);

            It should_set_method = () =>
                webRequest.Method.ShouldEqual(HttpMethod.Options);

            // It should_initialize_cookie_container = () =>
            //     webRequest.CookieContainer.ShouldNotBeNull();

            // It should_get_cookie_from_cookie_strategy = () =>
            //     The<ICookieStrategy>().WasToldTo(s => s.Get(Param.IsAny<IHttpRequest>()));
        }

        // [Subject(typeof(RequestRunner))]
        // public class when_building_web_request_with_custom_user_agent : with_request_runner
        // {
        //     Establish context = () =>
        //     {
        //         request.WhenToldTo(r => r.HasUserAgent).Return(true);
        //         request.WhenToldTo(r => r.UserAgent).Return(new UserAgent("custom user agent"));
        //     };

        //     Because of = () =>
        //         webRequest = Subject.BuildWebRequest(request);

        //     It should_add_header_to_web_request = () =>
        //         webRequest.UserAgent.ShouldEqual("custom user agent");

        //     static HttpWebRequest webRequest;
        // }

        // [Subject(typeof(RequestRunner))]
        // public class when_building_web_request_with_headers : with_request_runner
        // {
        //     Establish context = () =>
        //         request.WhenToldTo(r => r.Headers).Return(new[] { new Header("name", "value") });

        //     Because of = () =>
        //         webRequest = Subject.BuildWebRequest(request);

        //     It should_add_header_to_web_request = () =>
        //         webRequest.Headers["name"].ShouldEqual("value");

        //     static HttpWebRequest webRequest;
        // }

        // [Subject(typeof(RequestRunner))]
        // public class when_building_web_request : with_request_runner
        // {
        //     Because of = () =>
        //         Subject.BuildWebRequest(request);

        //     It should_authenticate_request = () =>
        //         The<IAuthenticator>().WasToldTo(a => a.Authenticate(Param.IsAny<IHttpRequest>()));
        // }

        // [Subject(typeof(RequestRunner))]
        // public class when_building_web_request_with_credentials : with_request_runner
        // {
        //     Establish context = () =>
        //         request.WhenToldTo(r => r.Credentials).Return(CredentialCache.DefaultCredentials);

        //     Because of = () =>
        //         webRequest = Subject.BuildWebRequest(request);

        //     It should_set_credentials_on_web_request = () =>
        //         webRequest.Credentials.ShouldNotBeNull();

        //     static HttpWebRequest webRequest;
        // }

        // [Subject(typeof(RequestRunner))]
        // public class when_creating_response : with_request_runner
        // {
        //     Establish context = () =>
        //     {
        //         webResponse = An<IHttpWebResponse>();
        //         webResponse.WhenToldTo(r => r.ContentType).Return("application/json");
        //     };

        //     Because of = () =>
        //         Subject.CreateHttpResponse(webResponse, new MemoryStream());

        //     It should_find_deserializer = () =>
        //         The<ITransmissionSettings>().WasToldTo(t => t.FindSerializer("application/json"));

        //     static IHttpWebResponse webResponse;
        // }

        // [Subject(typeof(PostRequest))]
        // public class when_building_web_request_with_no_body : with_request_runner
        // {
        //     Establish context = () =>
        //         request = new PostRequest(new Resource("http://example.com/companies"));

        //     Because of = () =>
        //         webRequest = Subject.BuildWebRequest(request);

        //     It should_have_post_method = () =>
        //         webRequest.Method.ShouldEqual("POST");

        //     static WebRequest webRequest;
        // }

        // [Subject(typeof(RequestRunner))]
        // public class when_building_web_request_with_reserved_headers : with_request_runner
        // {
        //     Establish context = () =>
        //         request.WhenToldTo(r => r.Headers).Return(new[] { new Header("Accept", "sup kids") });

        //     Because of = () =>
        //         webRequest = Subject.BuildWebRequest(request);

        //     It should_set_reserved_headers = () =>
        //         webRequest.Accept.ShouldEqual("sup kids");

        //     static HttpWebRequest webRequest;
        // }
    }
}
