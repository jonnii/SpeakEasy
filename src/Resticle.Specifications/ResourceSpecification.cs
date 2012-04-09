using System;
using Machine.Specifications;

namespace HttpSpeak.Specifications
{
    public class ResourceSpecification
    {
        [Subject(typeof(Resource))]
        public class when_creating_simple_resource
        {
            Establish context = () =>
                resource = new Resource("company");

            It should_create_resource_with_no_parameters = () =>
                resource.NumSegments.ShouldEqual(0);

            static Resource resource;
        }

        [Subject(typeof(Resource))]
        public class when_adding_parameters
        {
            Establish context = () =>
                resource = new Resource("company");

            Because of = () =>
                resource.AddParameter("filter", 5);

            It should_have_parameters = () =>
                resource.HasParameters.ShouldBeTrue();

            It should_have_parameter = () =>
                resource.HasParameter("filter");

            static Resource resource;
        }

        [Subject(typeof(Resource))]
        public class when_appending_resources
        {
            Establish context = () =>
                root = new Resource("http://example.com");

            Because of = () =>
                appended = root.Append(new Resource("api/companies"));

            It should_create_appended_resource = () =>
                appended.Path.ShouldEqual("http://example.com/api/companies");

            static Resource root;

            static Resource appended;
        }

        [Subject(typeof(Resource))]
        public class when_appending_resources_with_trailing_slash
        {
            Establish context = () =>
                root = new Resource("http://example.com/");

            Because of = () =>
                appended = root.Append(new Resource("api/companies"));

            It should_create_appended_resource = () =>
                appended.Path.ShouldEqual("http://example.com/api/companies");

            static Resource root;

            static Resource appended;
        }

        [Subject(typeof(Resource))]
        public class when_creating_resource_with_parameters : with_resource_with_parameter
        {
            It should_create_resource_with_parameter = () =>
                resource.HasSegment("name");

            It should_have_one_parameter = () =>
                resource.NumSegments.ShouldEqual(1);
        }

        [Subject(typeof(Resource))]
        public class when_merging_segments : with_resource_with_parameter
        {
            Because of = () =>
                merged = resource.Merge(new { name = "company-name" });

            It should_merge_values_into_resource = () =>
                merged.Path.ShouldEqual("company/company-name");

            static Resource merged;
        }

        [Subject(typeof(Resource))]
        public class when_merging_segments_of_different_case : with_resource_with_parameter
        {
            Because of = () =>
                merged = resource.Merge(new { Name = "company-name" });

            It should_merge_values_into_resource = () =>
                merged.Path.ShouldEqual("company/company-name");

            static Resource merged;
        }

        [Subject(typeof(Resource))]
        public class when_merging_null_segments
        {
            Establish context = () =>
                resource = new Resource("company");

            Because of = () =>
                merged = resource.Merge(null);

            It should_return_resource = () =>
                merged.Path.ShouldEqual("company");

            static Resource resource;

            static Resource merged;
        }

        [Subject(typeof(Resource))]
        public class when_merging_null_segments_when_resource_has_segments
        {
            Establish context = () =>
                resource = new Resource("company/:id");

            Because of = () =>
                exception = Catch.Exception(() => resource.Merge(null));

            It should_throw_exception = () =>
                exception.ShouldBeOfType<ArgumentException>();

            static Resource resource;

            static Exception exception;
        }

        [Subject(typeof(Resource))]
        public class when_merging_segments_as_parameters_when_no_segment_names_in_path
        {
            Establish context = () =>
                resource = new Resource("companies");

            Because of = () =>
                merged = resource.Merge(new { Filter = "nasdaq" });

            It should_add_parameters = () =>
                merged.HasParameter("Filter").ShouldBeTrue();

            static Resource resource;

            static Resource merged;
        }

        [Subject(typeof(Resource))]
        public class when_merging_extra_segments_add_as_parameters
        {
            Establish context = () =>
                resource = new Resource("company/:id");

            Because of = () =>
                merged = resource.Merge(new { id = 5, Filter = "ftse" });

            It should_merge_url_segments = () =>
                merged.Path.ShouldEqual("company/5");

            It should_have_parameter_with_original_casing = () =>
                merged.HasParameter("Filter").ShouldBeTrue();

            It should_only_merge_in_given_parameters = () =>
                merged.NumParameters.ShouldEqual(1);

            static Resource resource;

            static Resource merged;
        }

        [Subject(typeof(Resource))]
        public class when_calling_methods_on_dynamic_resources
        {
            Establish context = () =>
                resource = Resource.Create("companies/:id");

            Because of = () =>
                 url = resource.Id("ibm");

            It should_format_url = () =>
                url.ShouldEqual("companies/ibm");

            static dynamic resource;

            static string url;
        }

        public class with_resource_with_parameter
        {
            Establish context = () =>
                resource = new Resource("company/:name");

            protected static Resource resource;
        }
    }
}
