using System;
using System.Threading;
using System.Threading.Tasks;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpClient))]
    class HttpClientSpecification : WithFakes
    {
        static HttpClient Subject;

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
                Subject = new HttpClient("http://example.com", new HttpClientSettings(), The<IRequestRunner>());

                The<IRequestRunner>().WhenToldTo(r => r.RunAsync(Param.IsAny<IHttpRequest>(), Param.IsAny<CancellationToken>()))
                    .Return(Task.Factory.StartNew(() => An<IHttpResponse>()));

                The<INamingConvention>().WhenToldTo(r => r.ConvertPropertyNameToParameterName(Param.IsAny<string>()))
                    .Return((string original) => original);
            };

            class when_getting_collection_resource
            {
                Because of = () =>
                    Subject.Get("companies").Await();

                It should_send_request = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<GetRequest>.Matches(p => p.Resource.Path == "http://example.com/companies"), Param.IsAny<CancellationToken>()));
            }

            //class when_getting_collection_resource_with_custom_user_agent
            //{
            //    Establish context = () =>
            //        The<IUserAgent>().WhenToldTo(u => u.Name).Return("custom user agent");

            //    Because of = () =>
            //        Subject.Get("companies").Await();

            //    It should_send_request = () =>
            //        The<IRequestRunner>().WasToldTo(r =>
            //            r.RunAsync(Param<GetRequest>.Matches(p => p.UserAgent.Name == "custom user agent"), Param.IsAny<CancellationToken>()));
            //}

            class when_getting_specific_resource
            {
                Because of = () =>
                    Subject.Get("company/:id", new { id = 5 }).Await();

                It should_send_request = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<GetRequest>.Matches(p => p.Resource.Path == "http://example.com/company/5"), Param.IsAny<CancellationToken>()));
            }

            class when_getting_resource_on_client_with_parameterized_root
            {
                Establish context = () =>
                    Subject = new HttpClient("http://:company.example.com/api", new HttpClientSettings(), The<IRequestRunner>());

                Because of = () =>
                    Subject.Get("user/:id", new { company = "acme", id = 5 }).Await();

                It should_send_request = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<GetRequest>.Matches(p =>
                            p.Resource.Path == "http://acme.example.com/api/user/5"), Param.IsAny<CancellationToken>()));
            }

            class when_posting
            {
                Because of = () =>
                    Subject.Post(new { Name = "frobble" }, "user").Await();

                It should_dispatch_post_request = () =>
                    The<IRequestRunner>().WasToldTo(r => r.RunAsync(Param.IsAny<PostRequest>(), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_body_and_no_segments
            {
                Because of = () =>
                    Subject.Post(new { Id = "body", Name = "company-name" }, "company/:id").Await();

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
                    Subject.Post(new { Id = "body" }, "company/:id", new { Id = "segments", moreGarbage = true }).Await();

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
                    Subject.Post("companies").Await();

                It should_not_have_body_set = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Body is PostRequestBody), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_file
            {
                Because of = () =>
                    Subject.Post(An<IFile>(), "companies").Await();

                It should_have_files = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PostRequest>.Matches(p => p.Body is FileUploadBody), Param.IsAny<CancellationToken>()));
            }

            class when_posting_with_file_and_segments
            {
                Because of = () =>
                    Subject.Post(An<IFile>(), "companies/:id", new { id = 3, additionalProperty = "what's up" }).Await();

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
                    Subject.Put(An<IFile>(), "companies/:id", new { id = 3, additionalProperty = "what's up" }).Await();

                It should_include_additional_parameters_in_resource = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Resource.HasParameter("additionalProperty")), Param.IsAny<CancellationToken>()));
            }

            class when_putting
            {
                Because of = () =>
                    Subject.Put(new { Name = "frobble" }, "user").Await();

                It should_dispatch_put_request = () =>
                    The<IRequestRunner>().WasToldTo(r => r.RunAsync(Param.IsAny<PutRequest>(), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_body_and_no_segments
            {
                Because of = () =>
                    Subject.Put(new { Id = "body" }, "company/:id").Await();

                It should_use_body_as_segments = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p =>
                            p.Resource.Path == "http://example.com/company/body"), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_body_and_segments
            {
                Because of = () =>
                    Subject.Put(new { Id = "body" }, "company/:id", new { Id = "segments", moreGarbage = true }).Await();

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
                    Subject.Patch(new { Id = "body" }, "company/:id", new { Id = "segments", moreGarbage = true }).Await();

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
                    Subject.Put("companies").Await();

                It should_not_have_body_set = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Body is PostRequestBody), Param.IsAny<CancellationToken>()));
            }

            class when_putting_with_file
            {
                Because of = () =>
                    Subject.Put(An<IFile>(), "companies").Await();

                It should_have_files = () =>
                    The<IRequestRunner>().WasToldTo(r =>
                        r.RunAsync(Param<PutRequest>.Matches(p => p.Body is FileUploadBody), Param.IsAny<CancellationToken>()));
            }

            class when_deleting
            {
                Because of = () =>
                    Subject.Delete("user/5").Await();

                It should_dispatch_delete_request = () =>
                    The<IRequestRunner>().WasToldTo(r => r.RunAsync(Param.IsAny<DeleteRequest>(), Param.IsAny<CancellationToken>()));
            }
        }
    }
}
