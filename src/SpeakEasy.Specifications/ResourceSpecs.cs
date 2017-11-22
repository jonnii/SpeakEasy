using Machine.Specifications;
using SpeakEasy.ArrayFormatters;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(Resource))]
    class ResourceSpecs
    {
        static Resource root;

        static Resource appended;

        class when_creating_simple_resource
        {
            Establish context = () =>
                root = new Resource("company");

            It should_create_resource_with_no_parameters = () =>
                root.NumSegments.ShouldEqual(0);
        }

        class when_creating_with_trailing_slash
        {
            Establish context = () =>
                root = new Resource("http://example.com/");

            It should_trim_leading_slash = () =>
                root.Path.ShouldEqual("http://example.com");
        }

        class when_adding_parameters
        {
            Establish context = () =>
                root = new Resource("company");

            Because of = () =>
                root.AddParameter("filter", 5);

            It should_have_parameters = () =>
                root.HasParameters.ShouldBeTrue();

            It should_have_parameter = () =>
                root.HasParameter("filter");
        }

        class when_appending_resources
        {
            Establish context = () =>
                root = new Resource("http://example.com");

            Because of = () =>
                appended = root.Append(new Resource("api/companies"));

            It should_create_appended_resource = () =>
                appended.Path.ShouldEqual("http://example.com/api/companies");
        }

        class when_appending_resources_with_trailing_slash
        {
            Establish context = () =>
                root = new Resource("http://example.com/");

            Because of = () =>
                appended = root.Append(new Resource("api/companies"));

            It should_create_appended_resource = () =>
                appended.Path.ShouldEqual("http://example.com/api/companies");
        }

        class when_appending_resources_with_leading_slash
        {
            Establish context = () =>
                root = new Resource("http://example.com");

            Because of = () =>
                appended = root.Append(new Resource("/api/companies"));

            It should_create_appended_resource = () =>
                appended.Path.ShouldEqual("http://example.com/api/companies");
        }

        class when_appending_resources_with_leading_and_trailing_slashes
        {
            Establish context = () =>
                root = new Resource("http://example.com/");

            Because of = () =>
                appended = root.Append(new Resource("/api/companies"));

            It should_create_appended_resource = () =>
                appended.Path.ShouldEqual("http://example.com/api/companies");
        }

        class when_creating_resource_with_parameters
        {
            Establish context = () =>
                root = new Resource("company/:name");

            It should_create_resource_with_parameter = () =>
                root.HasSegment("name");

            It should_have_one_parameter = () =>
                root.NumSegments.ShouldEqual(1);
        }

        class when_getting_encoded_parameters
        {
            static string encoded;

            Establish context = () =>
            {
                root = new Resource("companies");
                root.AddParameter("name", "jim");
                root.AddParameter("age", "26");
            };

            Because of = () =>
                encoded = root.GetEncodedParameters(new CommaSeparatedArrayFormatter());

            It should_encode_parameters = () =>
                encoded.ShouldEqual("name=jim&age=26");
        }

        class when_getting_encoded_parameters_with_null_parameters
        {
            static string encoded;

            Establish context = () =>
            {
                root = new Resource("companies");
                root.AddParameter("name", "jim");
                root.AddParameter("age", null);
            };

            Because of = () =>
                encoded = root.GetEncodedParameters(new CommaSeparatedArrayFormatter());

            It should_encode_parameters = () =>
                encoded.ShouldEqual("name=jim");
        }

        class when_getting_encoded_parameters_with_all_null_parameters
        {
            static string encoded;

            Establish context = () =>
            {
                root = new Resource("companies");
                root.AddParameter("name", null);
                root.AddParameter("age", null);
            };

            Because of = () =>
                encoded = root.GetEncodedParameters(new CommaSeparatedArrayFormatter());

            It should_encode_parameters = () =>
                encoded.ShouldBeEmpty();
        }
    }
}
