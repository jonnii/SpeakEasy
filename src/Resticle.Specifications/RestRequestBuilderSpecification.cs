using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestRequestBuilderSpecification
    {
        [Subject(typeof(RestRequestBuilder))]
        public class when_creating_request
        {
            Establish context = () =>
                builder = new RestRequestBuilder(new Resource("http://example.com"), "companies");

            Because of = () =>
                request = builder.Build();

            It should_create_request = () =>
                request.ShouldNotBeNull();

            It should_set_request_url = () =>
                request.Url.ToString().ShouldEqual("http://example.com/companies");

            It should_not_have_body = () =>
                request.Body.ShouldBeNull();

            static RestRequestBuilder builder;

            static IRestRequest request;
        }

        [Subject(typeof(RestRequestBuilder))]
        public class when_creating_request_with_body
        {
            Establish context = () =>
                builder = new RestRequestBuilder(new Resource("http://example.com"), "companies")
                    .WithBody(new { thing = "thang" });

            Because of = () =>
                request = builder.Build();

            It should_have_body = () =>
                request.Body.ShouldNotBeNull();

            static RestRequestBuilder builder;

            static IRestRequest request;
        }

    }
}
