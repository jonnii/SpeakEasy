using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class RequestRunnerSpecification
    {
        [Subject(typeof(RequestRunner))]
        public class when_building_web_request_with_get_request : with_request_runner
        {
            Establish context = () =>
                request = new GetRequest(new Resource("http://example.com/companies"));

            Because of = () =>
                webRequest = Subject.BuildWebRequest(request);

            It should_set_url = () =>
                webRequest.RequestUri.ToString().ShouldEqual("http://example.com/companies");

            It should_set_request_to_get_request = () =>
                webRequest.Method.ShouldEqual("GET");

            static WebRequest webRequest;
        }

        [Subject(typeof(RequestRunner))]
        public class when_building_web_request : with_request_runner
        {
            Because of = () =>
                Subject.BuildWebRequest(request);

            It should_authenticate_request = () =>
                The<IAuthenticator>().WasToldTo(a => a.Authenticate(Param.IsAny<IHttpRequest>()));
        }

        //[Subject(typeof(RequestRunner))]
        //public class when_creating_response : with_request_runner
        //{
        //    Establish context = () =>
        //    {
        //        webResponse = An<IHttpWebResponse>();
        //        webResponse.WhenToldTo(r => r.ResponseUri).Return(new Uri("http://example.com/companies"));
        //        webResponse.WhenToldTo(r => r.ContentType).Return("application/json");
        //    };

        //    Because of = () =>
        //        response = Subject.CreateHttpResponse(webResponse);

        //    It should_have_response_url_corresponding_to_request = () =>
        //        response.RequestedUrl.ShouldEqual(new Uri("http://example.com/companies"));

        //    It should_find_deserializer = () =>
        //        The<ITransmissionSettings>().WasToldTo(t => t.FindSerializer("application/json"));

        //    static IHttpWebResponse webResponse;

        //    static IHttpResponse response;
        //}

        //[Subject(typeof(PostRequest))]
        //public class when_building_web_request_with_no_body : with_transmission
        //{
        //    Establish context = () =>
        //        request = new PostRequest(new Resource("http://example.com/companies"));

        //    Because of = () =>
        //        webRequest = request.BuildWebRequest(transmissionSettings);

        //    It should_have_post_method = () =>
        //        webRequest.Method.ShouldEqual("POST");

        //    It should_set_content_type_to_transmission_content_type = () =>
        //        webRequest.ContentType.ShouldEqual("application/json");

        //    static WebRequest webRequest;

        //    static PostRequest request;
        //}

        //[Subject(typeof(PostRequest))]
        //public class when_building_web_request_with_parameters : with_transmission
        //{
        //    Establish context = () =>
        //    {
        //        var resource = new Resource("http://example.com/companies");
        //        resource.AddParameter("name", "bob");
        //        resource.AddParameter("age", 26);

        //        request = new PostRequest(resource);
        //    };

        //    Because of = () =>
        //        webRequest = request.BuildWebRequest(transmissionSettings);

        //    It should_have_form_encoded_content_type = () =>
        //        webRequest.ContentType.ShouldEqual("application/x-www-form-urlencoded");

        //    static WebRequest webRequest;

        //    static PostRequest request;
        //}

        public class with_request_runner : WithSubject<RequestRunner>
        {
            Establish context = () =>
            {
                request = An<IHttpRequest>();
                request.WhenToldTo(r => r.BuildRequestUrl()).Return("http://example.com");
                request.WhenToldTo(r => r.HttpMethod).Return("GET");
            };

            protected static IHttpRequest request;
        }
    }
}