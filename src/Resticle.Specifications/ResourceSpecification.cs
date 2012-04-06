using Machine.Specifications;

namespace Resticle.Specifications
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
                merged.ShouldEqual("company/company-name");

            static string merged;
        }

        [Subject(typeof(Resource))]
        public class when_merging_segments_of_different_case : with_resource_with_parameter
        {
            Because of = () =>
                merged = resource.Merge(new { Name = "company-name" });

            It should_merge_values_into_resource = () =>
                merged.ShouldEqual("company/company-name");

            static string merged;
        }

        [Subject(typeof(Resource))]
        public class when_merging_null_segments
        {
            Establish context = () =>
                resource = new Resource("company");

            Because of = () =>
                merged = resource.Merge(null);

            It should_return_resource = () =>
                merged.ShouldEqual("company");

            static Resource resource;

            static string merged;
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
