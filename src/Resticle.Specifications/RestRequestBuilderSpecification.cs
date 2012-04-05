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

            static RestRequestBuilder builder;

            static IRestRequest request;
        }
    }
}
