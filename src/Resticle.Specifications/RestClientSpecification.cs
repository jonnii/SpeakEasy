using System.Linq;
using Machine.Fakes;
using Machine.Specifications;

using Resticle.Serializers;

namespace Resticle.Specifications
{
    public class RestClientSpecification
    {
        [Subject(typeof(RestClient))]
        public class in_general : WithSubject<RestClient>
        {
            It should_default_to_json_serializer = () =>
                Subject.DefaultSerializer().ShouldBeOfType<JsonDotNetSerializer>();
        }

        [Subject(typeof(RestClient))]
        public class when_getting_collection_resource : with_client
        {
            Because of = () =>
                Subject.Get("companies");

            It should_send_request = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<GetRestRequest>.Matches(p => p.Resource.Path == "http://example.com/companies")));
        }

        [Subject(typeof(RestClient))]
        public class when_getting_specific_resource : with_client
        {
            Because of = () =>
                Subject.Get("company/:id", new { id = 5 });

            It should_send_request = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<GetRestRequest>.Matches(p => p.Resource.Path == "http://example.com/company/5")));
        }

        [Subject(typeof(RestClient))]
        public class when_getting_resource_on_rest_client_with_parameterized_root : with_client
        {
            Establish context = () =>
                Subject.Root = new Resource("http://:company.example.com/api");

            Because of = () =>
                Subject.Get("user/:id", new { company = "acme", id = 5 });

            It should_send_request = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<GetRestRequest>.Matches(p => p.Resource.Path == "http://acme.example.com/api/user/5")));
        }

        [Subject(typeof(RestClient))]
        public class when_posting : with_client
        {
            Because of = () =>
                Subject.Post(new { Name = "frobble" }, "user");

            It should_dispatch_post_request = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param.IsAny<PostRestRequest>()));
        }

        [Subject(typeof(RestClient))]
        public class when_posting_with_body_and_no_segments : with_client
        {
            Because of = () =>
                Subject.Post(new { Id = "body", Name = "company-name" }, "company/:id");

            It should_use_body_as_segments = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PostRestRequest>.Matches(p => p.Resource.Path == "http://example.com/company/body")));

            It should_not_add_extra_body_properties_as_parameters = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PostRestRequest>.Matches(p => !p.Resource.HasParameters)));
        }

        [Subject(typeof(RestClient))]
        public class when_posting_with_body_and_segments : with_client
        {
            Because of = () =>
                Subject.Post(new { Id = "body" }, "company/:id", new { Id = "segments" });

            It should_use_segments = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PostRestRequest>.Matches(p => p.Resource.Path == "http://example.com/company/segments")));
        }

        [Subject(typeof(RestClient))]
        public class when_posting_without_body : with_client
        {
            Because of = () =>
                Subject.Post("companies");

            It should_not_have_body_set = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PostRestRequest>.Matches(p => p.Body == null)));
        }

        [Subject(typeof(RestClient))]
        public class when_posting_with_file : with_client
        {
            Because of = () =>
                Subject.Post(new FileUpload("file.txt"), "companies");

            It should_have_files = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PostRestRequest>.Matches(p => p.Files.Any())));
        }

        [Subject(typeof(RestClient))]
        public class when_putting : with_client
        {
            Because of = () =>
                Subject.Put(new { Name = "frobble" }, "user");

            It should_dispatch_put_request = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param.IsAny<PutRestRequest>()));
        }

        [Subject(typeof(RestClient))]
        public class when_putting_with_body_and_no_segments : with_client
        {
            Because of = () =>
                Subject.Put(new { Id = "body" }, "company/:id");

            It should_use_body_as_segments = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PutRestRequest>.Matches(p => p.Resource.Path == "http://example.com/company/body")));
        }

        [Subject(typeof(RestClient))]
        public class when_putting_with_body_and_segments : with_client
        {
            Because of = () =>
                Subject.Put(new { Id = "body" }, "company/:id", new { Id = "segments" });

            It should_use_segments = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PutRestRequest>.Matches(p => p.Resource.Path == "http://example.com/company/segments")));
        }

        [Subject(typeof(RestClient))]
        public class when_putting_without_body : with_client
        {
            Because of = () =>
                Subject.Put("companies");

            It should_not_have_body_set = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PutRestRequest>.Matches(p => p.Body == null)));
        }

        [Subject(typeof(RestClient))]
        public class when_putting_with_file : with_client
        {
            Because of = () =>
                Subject.Put(new FileUpload("file.txt"), "companies");

            It should_have_files = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param<PutRestRequest>.Matches(p => p.Files.Any())));
        }

        [Subject(typeof(RestClient))]
        public class when_deleting : with_client
        {
            Because of = () =>
                Subject.Delete("user/5");

            It should_dispatch_delete_request = () =>
                The<IRequestRunner>().WasToldTo(r => r.Run(Param.IsAny<DeleteRestRequest>()));
        }

        public class with_client : WithSubject<RestClient>
        {
            Establish context = () =>
            {
                Subject.Root = new Resource("http://example.com");
            };
        }
    }
}
