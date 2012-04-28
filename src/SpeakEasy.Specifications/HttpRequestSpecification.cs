using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class HttpRequestSpecification
    {
        //[Subject(typeof(HttpRequest))]
        //public class when_building_web_request : with_request
        //{
        //    Because of = () =>
        //        webRequest = request.BuildWebRequest(transmissionSettings);

        //    It should_set_content_type = () =>
        //        webRequest.ContentType.ShouldEqual("text/xml");

        //    It should_set_supported_accept_types = () =>
        //        webRequest.Accept.ShouldEqual("text/xml, application/json");

        //    static HttpWebRequest webRequest;
        //}

        //[Subject(typeof(HttpRequest))]
        //public class when_building_web_request_with_headers : with_request
        //{
        //    Establish context = () =>
        //        request.AddHeader("name", "value");

        //    Because of = () =>
        //        webRequest = request.BuildWebRequest(transmissionSettings);

        //    It should_add_header_to_web_request = () =>
        //        webRequest.Headers["name"].ShouldEqual("value");

        //    static HttpWebRequest webRequest;
        //}

        //[Subject(typeof(HttpRequest))]
        //public class when_building_web_request_with_custom_user_agent : with_request
        //{
        //    Establish context = () =>
        //        request.UserAgent = "custom user agent";

        //    Because of = () =>
        //        webRequest = request.BuildWebRequest(transmissionSettings);

        //    It should_add_header_to_web_request = () =>
        //        webRequest.UserAgent.ShouldEqual("custom user agent");

        //    static HttpWebRequest webRequest;
        //}

        [Subject(typeof(HttpRequest))]
        public class in_general : with_request
        {
            It should_allow_auto_redirects = () =>
                request.AllowAutoRedirect.ShouldBeTrue();
        }

        [Subject(typeof(HttpRequest))]
        public class when_adding_header : with_request
        {
            Because of = () =>
                request.AddHeader("header", "value");

            It should_add_header = () =>
                request.NumHeaders.ShouldEqual(1);
        }

        //[Subject(typeof(HttpRequest))]
        //public class when_building_web_request_with_credentials : with_request
        //{
        //    Establish context = () =>
        //        request.Credentials = CredentialCache.DefaultCredentials;

        //    Because of = () =>
        //        webRequest = request.BuildWebRequest(transmissionSettings);

        //    It should_set_credentials_on_web_request = () =>
        //        webRequest.Credentials.ShouldNotBeNull();

        //    static HttpWebRequest webRequest;
        //}

        public class with_request : WithFakes
        {
            Establish context = () =>
            {
                transmissionSettings = An<ITransmissionSettings>();
                transmissionSettings.WhenToldTo(t => t.DefaultSerializerContentType).Return("text/xml");
                transmissionSettings.WhenToldTo(t => t.DeserializableMediaTypes).Return(new[] { "text/xml", "application/json" });

                request = new TestHttpRequest(new Resource("http://example.com/api/companies"));
            };

            protected static TestHttpRequest request;

            protected static ITransmissionSettings transmissionSettings;
        }
    }
}