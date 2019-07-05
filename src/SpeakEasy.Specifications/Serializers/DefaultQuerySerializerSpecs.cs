using System.Collections.Generic;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Serializers
{
    [Subject(typeof(DefaultQuerySerializer))]
    class DefaultQuerySerializerSpecs
    {
        static DefaultQuerySerializer serializer = new DefaultQuerySerializer();

        static IEnumerable<string> formatted;

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
    }
}
