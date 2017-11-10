using System;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(ResourceMerger))]
    class ResourceMergerSpecification : WithSubject<ResourceMerger>
    {
        static Resource resource;

        static Resource merged;

        static Exception exception;

        class with_resource_with_parameter
        {
            Establish context = () =>
            {
                resource = new Resource("company/:name");
                Subject.NamingConvention = new DefaultNamingConvention();
            };

            class when_merging_segments
            {
                Because of = () =>
                    merged = Subject.Merge(resource, new { name = "company-name" });

                It should_merge_values_into_resource = () =>
                    merged.Path.ShouldEqual("company/company-name");
            }

            class when_merging_segments_with_null_value
            {

                Because of = () =>
                    exception = Catch.Exception(() => Subject.Merge(resource, new { name = (string)null }));

                It should_throw_exception = () =>
                    exception.ShouldBeOfExactType<ArgumentException>();
            }

            class when_merging_segments_of_different_case
            {
                Because of = () =>
                    merged = Subject.Merge(resource, new { Name = "company-name" });

                It should_merge_values_into_resource = () =>
                    merged.Path.ShouldEqual("company/company-name");
            }
        }

        class with_multiple_resource_with_parameters
        {
            Establish context = () =>
            {
                resource = new Resource("company/:name/:companyType");
                Subject.NamingConvention = new DefaultNamingConvention();
            };

            class when_merging_multiple_segments
            {
                Because of = () =>
                    merged = Subject.Merge(resource, new { name = "company-name", companyType = "public" });

                It should_merge_values_into_resource = () =>
                    merged.Path.ShouldEqual("company/company-name/public");
            }
        }

        class when_merging_null_segments
        {
            Establish context = () =>
                resource = new Resource("company");

            Because of = () =>
                merged = Subject.Merge(resource, null);

            It should_return_resource = () =>
                merged.Path.ShouldEqual("company");
        }

        class when_merging_null_segments_when_resource_has_segments
        {
            Establish context = () =>
                resource = new Resource("company/:id");

            Because of = () =>
                exception = Catch.Exception(() => Subject.Merge(resource, null));

            It should_throw_exception = () =>
                exception.ShouldBeOfExactType<ArgumentException>();
        }

        class when_merging_segments_as_parameters_when_no_segment_names_in_path
        {
            Establish context = () =>
            {
                Subject.NamingConvention = new DefaultNamingConvention();
                resource = new Resource("companies");
            };

            Because of = () =>
                merged = Subject.Merge(resource, new { Filter = "nasdaq" });

            It should_add_parameters = () =>
                merged.HasParameter("Filter").ShouldBeTrue();
        }

        class when_merging_extra_segments_add_as_parameters
        {
            Establish context = () =>
            {
                Subject.NamingConvention = new DefaultNamingConvention();
                resource = new Resource("company/:id");
            };

            Because of = () =>
                merged = Subject.Merge(resource, new { id = 5, Filter = "ftse" });

            It should_merge_url_segments = () =>
                merged.Path.ShouldEqual("company/5");

            It should_have_parameter_with_original_casing = () =>
                merged.HasParameter("Filter").ShouldBeTrue();

            It should_only_merge_in_given_parameters = () =>
                merged.NumParameters.ShouldEqual(1);
        }
    }
}