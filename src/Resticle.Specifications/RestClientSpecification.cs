using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestClientSpecification
    {
        [Subject(typeof(RestClient))]
        public class when_getting_collection_resource : with_client
        {
            Because of = () =>
                response = client.Get("companies");

            It should_create_rest_response = () =>
                response.ShouldNotBeNull();

            It should_have_rest_response_with_full_resource = () =>
                response.RequestedUri.ToString().ShouldEqual("http://example.com/companies");

            static IRestResponse response;
        }

        [Subject(typeof(RestClient))]
        public class when_getting_specific_resource : with_client
        {
            Because of = () =>
                response = client.Get("company/:id", new { id = 5 });

            It should_have_rest_response_with_full_resource = () =>
                response.RequestedUri.ToString().ShouldEqual("http://example.com/company/5");

            static IRestResponse response;
        }

        [Subject(typeof(RestClient))]
        public class when_getting_resource_on_rest_client_with_parameterized_root
        {
            Establish context = () =>
                client = new RestClient("http://:company.example.com/api");

            Because of = () =>
                response = client.Get("user/:id", new { company = "acme", id = 5 });

            It should_have_rest_response_with_full_resource = () =>
                response.RequestedUri.ToString().ShouldEqual("http://acme.example.com/api/user/5");

            static RestClient client;

            static IRestResponse response;
        }
    }

    public class with_client
    {
        Establish context = () =>
            client = new RestClient("http://example.com");

        protected static RestClient client;
    }
}
