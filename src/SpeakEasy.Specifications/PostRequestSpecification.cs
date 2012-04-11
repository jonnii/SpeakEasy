using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class PostRequestSpecification
    {
        [Subject(typeof(PostRequest))]
        public class in_general_without_body
        {
            Establish context = () =>
                request = new PostRequest(new Resource("http://example.com/companies"));

            It should_have_null_body = () =>
                request.Body.ShouldBeOfType<DefaultRequestBody>();

            static PostRequest request;
        }

        [Subject(typeof(PostRequest))]
        public class when_building_web_request_with_no_body : with_transmission
        {
            Establish context = () =>
                request = new PostRequest(new Resource("http://example.com/companies"));

            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_post_method = () =>
                webRequest.Method.ShouldEqual("POST");

            It should_set_content_type_to_transmission_content_type = () =>
                webRequest.ContentType.ShouldEqual("application/json");

            static WebRequest webRequest;

            static PostRequest request;
        }

        [Subject(typeof(PostRequest))]
        public class when_building_web_request_with_parameters : with_transmission
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("name", "bob");
                resource.AddParameter("age", 26);

                request = new PostRequest(resource);
            };

            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_form_encoded_content_type = () =>
                webRequest.ContentType.ShouldEqual("application/x-www-form-urlencoded");

            static WebRequest webRequest;

            static PostRequest request;
        }

        public class with_transmission : WithFakes
        {
            Establish context = () =>
            {
                transmissionSettings = An<ITransmissionSettings>();
                transmissionSettings.WhenToldTo(r => r.DefaultSerializerContentType).Return("application/json");
            };

            protected static ITransmissionSettings transmissionSettings;
        }
    }
}
