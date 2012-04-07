using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class PostRestRequestSpecification
    {
        [Subject(typeof(PostRestRequest))]
        public class in_general_without_body
        {
            Establish context = () =>
                request = new PostRestRequest(new Resource("http://example.com/companies"), null);

            It should_not_have_body = () =>
                request.HasSerializableBody.ShouldBeFalse();

            static PostRestRequest request;
        }

        [Subject(typeof(PostRestRequest))]
        public class in_general_with_body : with_transmission
        {
            Establish context = () =>
                request = new PostRestRequest(new Resource("http://example.com/companies"), "awesome sauce");

            It should_have_body = () =>
                request.HasSerializableBody.ShouldBeTrue();

            static PostRestRequest request;
        }

        [Subject(typeof(PostRestRequest))]
        public class in_general_with_parameters : with_transmission
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("name", "bob");

                request = new PostRestRequest(resource, null);
            };

            It should_have_body = () =>
                request.HasSerializableBody.ShouldBeTrue();

            static PostRestRequest request;
        }

        [Subject(typeof(PostRestRequest))]
        public class when_building_web_request_with_no_body : with_transmission
        {
            Establish context = () =>
                request = new PostRestRequest(new Resource("http://example.com/companies"), null);

            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_post_method = () =>
                webRequest.Method.ShouldEqual("POST");

            It should_set_content_type_to_transmission_content_type = () =>
                webRequest.ContentType.ShouldEqual("application/json");

            static WebRequest webRequest;

            static PostRestRequest request;
        }

        [Subject(typeof(PostRestRequest))]
        public class when_building_web_request_with_parameters : with_transmission
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("name", "bob");
                resource.AddParameter("age", 26);

                request = new PostRestRequest(resource, null);
            };

            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_form_encoded_content_type = () =>
                webRequest.ContentType.ShouldEqual("application/x-www-form-urlencoded");

            static WebRequest webRequest;

            static PostRestRequest request;
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
