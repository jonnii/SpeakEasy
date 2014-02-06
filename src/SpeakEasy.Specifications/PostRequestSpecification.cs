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
                request.Body.ShouldBeOfType<PostRequestBody>();

            static PostRequest request;
        }

        [Subject(typeof(PostRequest))]
        public class when_building_request_url_with_object_body
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("makemoney", "allday");

                request = new PostRequest(resource, new ObjectRequestBody(new { }));
            };

            It should_generate_query_params = () =>
                request.BuildRequestUrl(new CommaSeparatedArrayFormatter()).ShouldEqual("http://example.com/companies?makemoney=allday");

            static PostRequest request;
        }

        [Subject(typeof(PostRequest))]
        public class when_building_request_url_with_post_request_body
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("makemoney", "allday");

                request = new PostRequest(resource, new PostRequestBody(new Resource("foo")));
            };

            It should_not_generate_query_params = () =>
                request.BuildRequestUrl(new CommaSeparatedArrayFormatter()).ShouldEqual("http://example.com/companies");

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
