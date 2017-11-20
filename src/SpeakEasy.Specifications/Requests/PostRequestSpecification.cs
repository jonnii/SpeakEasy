using Machine.Specifications;
using SpeakEasy.ArrayFormatters;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(PostRequest))]
    class PostRequestSpecification
    {
        static PostRequest request;

        class in_general_without_body
        {
            Establish context = () =>
                request = new PostRequest(new Resource("http://example.com/companies"));

            It should_have_null_body = () =>
                request.Body.ShouldBeOfExactType<PostRequestBody>();
        }

        class when_building_request_url_with_object_body
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("makemoney", "allday");

                request = new PostRequest(resource, new ObjectRequestBody(new { }));
            };

            It should_generate_query_params = () =>
                request.BuildRequestUrl(new CommaSeparatedArrayFormatter()).ShouldEqual("http://example.com/companies?makemoney=allday");
        }

        class when_building_request_url_with_post_request_body
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("makemoney", "allday");

                request = new PostRequest(resource, new PostRequestBody(new Resource("foo")));
            };

            It should_not_generate_query_params = () =>
                request.BuildRequestUrl(new CommaSeparatedArrayFormatter()).ShouldEqual("http://example.com/companies");
        }
    }
}
