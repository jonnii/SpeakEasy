using System;
using System.Collections.Generic;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Serializers
{
    [Subject(typeof(DefaultQuerySerializer))]
    class DefaultQuerySerializerSpecs
    {
        static DefaultQuerySerializer serializer;

        static IEnumerable<string> formatted;

        Establish context = () =>
            serializer = new DefaultQuerySerializer();

        class in_general
        {
            It should_expand_array_values = () =>
                serializer.ExpandArrayValues.ShouldBeTrue();
        }

        class when_converting_to_query_string
        {
            Because of = () =>
                formatted = serializer.FormatParameters(new []
                {
                    new Parameter("name", "value")
                });

            It should_format_as_query_string = () =>
                formatted.ShouldContain("name=value");
        }

        class when_expanding_array_values
        {
            class when_converting_to_query_string_with_int_array_value_and_multiple_values_array_formatter
            {
                Establish context = () =>
                    serializer.ExpandArrayValues = true;

                Because of = () =>
                    formatted = serializer.FormatParameters(new []
                    {
                        new Parameter("name", new[] { 3, 4, 5 })
                    });

                It should_format_as_query_string = () =>
                    formatted.ShouldContain("name=3", "name=4", "name=5");
            }
        }

        class when_not_expanding_array_values
        {
            Establish context = () =>
                serializer.ExpandArrayValues = false;

            class when_converting_to_query_string_with_string_array_value
            {
                Because of = () =>
                    formatted = serializer.FormatParameters(new []
                    {
                        new Parameter("name", new[] { "value1", "value2" })
                    });

                It should_format_as_query_string = () =>
                    formatted.ShouldContain("name=value1,value2");
            }

            class when_converting_to_query_string_with_int_array_value
            {
                Because of = () =>
                    formatted = serializer.FormatParameters(new []
                    {
                        new Parameter("name", new[] { 3, 4, 5 })
                    });

                It should_format_as_query_string = () =>
                    formatted.ShouldContain("name=3,4,5");
            }
        }

        class when_converting_to_query_string_with_date_time
        {
            Because of = () =>
                formatted = serializer.FormatParameters(new[]
                {
                    new Parameter("name", new DateTime(2013, 10, 15, 14, 30, 44))
                });

            It should_format_as_query_string = () =>
                formatted.ShouldContain("name=2013-10-15T14:30:44.0000000");
        }

        class when_converting_to_query_string_with_nullable_date_time
        {
            Because of = () =>
                formatted = serializer.FormatParameters(new []
                {
                    new Parameter("name", (DateTime?)new DateTime(2013, 10, 15, 14, 30, 44, DateTimeKind.Utc))
                });

            It should_format_as_query_string = () =>
                formatted.ShouldContain("name=2013-10-15T14:30:44.0000000Z");
        }

        class when_converting_values_containing_slashes_and_ampersands
        {
            Because of = () =>
                formatted = serializer.FormatParameters(new []
                {
                    new Parameter("name", "value/this&that")
                });

            It should_properly_escape_special_characters = () =>
                formatted.ShouldContain("name=value%2Fthis%26that");
        }
    }
}
