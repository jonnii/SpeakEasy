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

        public class with_resource_with_parameter
        {
            Establish context = () =>
                resource = new Resource("company/:name");

            protected static Resource resource;
        }
    }
}
