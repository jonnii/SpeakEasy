using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications
{
    using SystemHttpClient = System.Net.Http.HttpClient;

    [Subject(typeof(HttpClient))]
    class HttpClientSpecs : WithFakes
    {
        static HttpClient client;

        class when_creating_with_invalid_settings
        {
            static HttpClientSettings settings;

            static Exception exception;

            Establish context = () =>
            {
                settings = new HttpClientSettings();
                settings.Serializers.Clear();
            };

            Because of = () =>
                exception = Catch.Exception(() => HttpClient.Create("http://example.com/api", settings));

            It should_throw = () =>
                exception.ShouldBeOfExactType<InvalidOperationException>();
        }

        class with_client
        {
            Establish context = () =>
            {
                client = new HttpClient("http://example.com", new HttpClientSettings(), The<IRequestRunner>());

                The<IRequestRunner>().WhenToldTo(r => r.RunAsync(Param.IsAny<IHttpRequest>(), Param.IsAny<CancellationToken>()))
                    .Return(Task.Factory.StartNew(() => An<IHttpResponse>()));

                The<INamingConvention>().WhenToldTo(r => r.ConvertPropertyNameToParameterName(Param.IsAny<string>()))
                    .Return((string original) => original);
            };

            class when_building_system_client
            {
                static SystemHttpClient system_http_client;

                Because of = () =>
                    system_http_client = client.BuildSystemClient(new CookieContainer());

                It should_create_client = () =>
                    system_http_client.ShouldNotBeNull();
            }

            class when_getting_collection_resource
            {
                Because of = () =>
                    client.Get("companies").Await();

                It should_send_request = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<GetRequest>.Matches(p => p.Resource.Path == "http://example.com/companies"), Param.IsAny<CancellationToken>()));
            }

            class when_getting_specific_resource
            {
                Because of = () =>
                    client.Get("company/:id", new { id = 5 }).Await();

                It should_send_request = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<GetRequest>.Matches(p => p.Resource.Path == "http://example.com/company/5"), Param.IsAny<CancellationToken>()));
            }

            class when_getting_resource_on_client_with_parameterized_root
            {
                Establish context = () =>
                    client = new HttpClient("http://:company.example.com/api", new HttpClientSettings(), The<IRequestRunner>());

                Because of = () =>
                    client.Get("user/:id", new { company = "acme", id = 5 }).Await();

                It should_send_request = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<GetRequest>.Matches(p =>
                            p.Resource.Path == "http://acme.example.com/api/user/5"), Param.IsAny<CancellationToken>()));
            }

            class when_posting
            {
                Because of = () =>
                    client.Post(new { Name = "frobble" }, "user").Await();

                It should_dispatch_post_request = () =>
                    The<IRequestRunner>().WasToldTo(r => r.RunAsync(Param.IsAny<PostRequest>(), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_body_and_no_segments
            {
                Because of = () =>
                    client.Post(new { Id = "body", Name = "company-name" }, "company/:id").Await();

                It should_use_body_as_segments = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(
                            Param<PostRequest>.Matches(p => p.Resource.Path == "http://example.com/company/body"), Param.IsAny<CancellationToken>()));

                It should_not_add_extra_body_properties_as_parameters = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => !p.Resource.HasParameters), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_body_and_segments
            {
                Because of = () =>
                    client.Post(new { Id = "body" }, "company/:id", new { Id = "segments", moreGarbage = true }).Await();

                It should_use_segments = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p =>
                            p.Resource.Path == "http://example.com/company/segments"), Param.IsAny<CancellationToken>()));

                It should_add_extra_segment_properties_as_parameters = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Resource.HasParameter("moreGarbage")), Param.IsAny<CancellationToken>()));
            }

            class when_posting_without_body
            {
                Because of = () =>
                    client.Post("companies").Await();

                It should_not_have_body_set = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Body is PostRequestBody), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_file
            {
                Because of = () =>
                    client.Post(An<IFile>(), "companies").Await();

                It should_have_files = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Body is FileUploadBody), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_file_and_segments
            {
                Because of = () =>
                    client.Post(An<IFile>(), "companies/:id", new { id = 3, additionalProperty = "what's up" }).Await();

                It should_have_files = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Body is FileUploadBody), Param.IsAny<CancellationToken>()));

                It should_merge_url_parameters = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p =>
                            p.Resource.Path == "http://example.com/companies/3"), Param.IsAny<CancellationToken>()));

                It should_include_additional_parameters_in_resource = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Resource.HasParameter("additionalProperty")), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_file_and_segments
            {
                Because of = () =>
                    client.Put(An<IFile>(), "companies/:id", new { id = 3, additionalProperty = "what's up" }).Await();

                It should_include_additional_parameters_in_resource = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Resource.HasParameter("additionalProperty")), Param.IsAny<CancellationToken>()));
            }

            class when_putting
            {
                Because of = () =>
                    client.Put(new { Name = "frobble" }, "user").Await();

                It should_dispatch_put_request = () =>
                    The<IRequestRunner>().WasToldTo(r => r.RunAsync(Param.IsAny<PutRequest>(), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_body_and_no_segments
            {
                Because of = () =>
                    client.Put(new { Id = "body" }, "company/:id").Await();

                It should_use_body_as_segments = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p =>
                            p.Resource.Path == "http://example.com/company/body"), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_body_and_segments
            {
                Because of = () =>
                    client.Put(new { Id = "body" }, "company/:id", new { Id = "segments", moreGarbage = true }).Await();

                It should_use_segments = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p =>
                            p.Resource.Path == "http://example.com/company/segments"), Param.IsAny<CancellationToken>()));

                It should_add_extra_segment_properties_as_parameters = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Resource.HasParameter("moreGarbage")), Param.IsAny<CancellationToken>()));
            }

            class when_patching_with_body_and_segments
            {
                Because of = () =>
                    client.Patch(new { Id = "body" }, "company/:id", new { Id = "segments", moreGarbage = true }).Await();

                It should_use_segments = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PatchRequest>.Matches(p =>
                            p.Resource.Path == "http://example.com/company/segments"), Param.IsAny<CancellationToken>()));

                It should_add_extra_segment_properties_as_parameters = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PatchRequest>.Matches(p => p.Resource.HasParameter("moreGarbage")), Param.IsAny<CancellationToken>()));
            }

            class when_putting_without_body
            {
                Because of = () =>
                    client.Put("companies").Await();

                It should_not_have_body_set = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Body is PostRequestBody), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_file
            {
                Because of = () =>
                    client.Put(An<IFile>(), "companies").Await();

                It should_have_files = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Body is FileUploadBody), Param.IsAny<CancellationToken>()));
            }

            class when_deleting
            {
                Because of = () =>
                    client.Delete("user/5").Await();

                It should_dispatch_delete_request = () =>
                    The<IRequestRunner>().WasToldTo(r => r.RunAsync(Param.IsAny<DeleteRequest>(), Param.IsAny<CancellationToken>()));
            }
        }
    }
}
