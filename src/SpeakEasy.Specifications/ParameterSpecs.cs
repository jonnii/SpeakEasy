using System;
using Machine.Specifications;
using SpeakEasy.ArrayFormatters;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(Parameter))]
    class ParameterSpecs
    {
        static Parameter parameter;

        static string formatted;

        class when_converting_to_query_string
        {
            Establish context = () =>
                parameter = new Parameter("name", "value");

            Because of = () =>
                formatted = parameter.ToQueryString(new CommaSeparatedArrayFormatter());

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=value");
        }

        class when_converting_to_query_string_with_string_array_value
        {
            Establish context = () =>
                parameter = new Parameter("name", new[] { "value1", "value2" });

            Because of = () =>
                formatted = parameter.ToQueryString(new CommaSeparatedArrayFormatter());

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=value1,value2");
        }

        class when_converting_to_query_string_with_int_array_value
        {
            Establish context = () =>
                parameter = new Parameter("name", new[] { 3, 4, 5 });

            Because of = () =>
                formatted = parameter.ToQueryString(new CommaSeparatedArrayFormatter());

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=3,4,5");
        }

        class when_converting_to_query_string_with_int_array_value_and_multiple_values_array_formatter
        {
            Establish context = () =>
                parameter = new Parameter("name", new[] { 3, 4, 5 });

            Because of = () =>
                formatted = parameter.ToQueryString(new MultipleValuesArrayFormatter());

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=3&name=4&name=5");
        }

        class when_converting_to_query_string_with_date_time
        {
            Establish context = () =>
                parameter = new Parameter("name", new DateTime(2013, 10, 15, 14, 30, 44));

            Because of = () =>
                formatted = parameter.ToQueryString(new CommaSeparatedArrayFormatter());

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=2013-10-15T14:30:44.0000000");
        }

        class when_converting_to_query_string_with_nullable_date_time
        {
            Establish context = () =>
                parameter = new Parameter("name", (DateTime?)new DateTime(2013, 10, 15, 14, 30, 44, DateTimeKind.Utc));

            Because of = () =>
                formatted = parameter.ToQueryString(new CommaSeparatedArrayFormatter());

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=2013-10-15T14:30:44.0000000Z");
        }

        class when_value_is_nullable
        {
            Establish context = () =>
                parameter = new Parameter("name", new DateTime?());

            It should_not_have_value = () =>
                parameter.HasValue.ShouldBeFalse();
        }
    }
}
