using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(RequestRunner))]
    class RequestRunnerSpecs : WithSubject<RequestRunner>
    {
        static HttpRequestMessage webRequest;

        static IHttpRequest request;

        Establish context = () =>
        {
            request = An<IHttpRequest>();
            request.WhenToldTo(r => r.BuildRequestUrl(Param.IsAny<IArrayFormatter>())).Return("http://example.com");
            request.WhenToldTo(r => r.HttpMethod).Return(HttpMethod.Get);
        };

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
    }
}
