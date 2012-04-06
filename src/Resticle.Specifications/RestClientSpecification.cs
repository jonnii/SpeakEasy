using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestClientSpecification
    {
        [Subject(typeof(RestClient))]
        public class in_general
        {
            Establish context = () =>
                client = new RestClient();

            It should_have_dispatcher = () =>
                client.Dispatcher.ShouldNotBeNull();

            static RestClient client;
        }

        [Subject(typeof(RestClient))]
        public class when_creating_new_request : with_client
        {
            Because of = () =>
                builder = client.NewRequest("company/:id", new { id = 5 });

            It should_create_request_builder = () =>
                builder.ShouldNotBeNull();

            static IRestRequestBuilder builder;
        }

        [Subject(typeof(RestClient))]
        public class when_getting_collection_resource : with_client
        {
            Because of = () =>
                client.Get("companies");

            It should_dispatch_request = () =>
                dispatcher.WasToldTo(d => d.Dispatch(Param<GetRestRequest>.Matches(
                    r => r.Url.ToString() == "http://example.com/companies")));
        }

        [Subject(typeof(RestClient))]
        public class when_getting_specific_resource : with_client
        {
            Because of = () =>
                client.Get("company/:id", new { id = 5 });

            It should_dispatch_request = () =>
                dispatcher.WasToldTo(d => d.Dispatch(Param<GetRestRequest>.Matches(
                    r => r.Url.ToString() == "http://example.com/company/5")));
        }

        [Subject(typeof(RestClient))]
        public class when_getting_resource_on_rest_client_with_parameterized_root : with_dispatcher
        {
            Establish context = () =>
                client = new RestClient("http://:company.example.com/api")
                {
                    Dispatcher = dispatcher
                };

            Because of = () =>
                client.Get("user/:id", new { company = "acme", id = 5 });

            It should_dispatch_request = () =>
                dispatcher.WasToldTo(d => d.Dispatch(Param<GetRestRequest>.Matches(
                    r => r.Url.ToString() == "http://acme.example.com/api/user/5")));

            static RestClient client;
        }

        [Subject(typeof(RestClient))]
        public class when_posting : with_client
        {
            Because of = () =>
                client.Post(new { Name = "frobble" }, "user");

            It should_dispatch_post_request = () =>
                dispatcher.WasToldTo(d => d.Dispatch(Param.IsAny<PostRestRequest>()));
        }

        public class with_dispatcher : WithFakes
        {
            Establish context = () =>
                dispatcher = An<IRestRequestDispatcher>();

            protected static IRestRequestDispatcher dispatcher;
        }

        public class with_client : with_dispatcher
        {
            Establish context = () =>
            {
                client = new RestClient("http://example.com")
                {
                    Dispatcher = dispatcher
                };
            };

            protected static RestClient client;
        }
    }
}
