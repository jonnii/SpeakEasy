using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.ArrayFormatters;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(MultipleValuesArrayFormatter))]
    class MultipleValuesArrayFormatterSpecs : WithSubject<MultipleValuesArrayFormatter>
    {
        class when_formatting
        {
            static string formatted;

            Because of = () =>
                formatted = Subject.FormatParameter("field", new[] { "a", "b", "c" }, t => t.ToString());

            It should_format_each_item = () =>
                formatted.ShouldEqual("field=a&field=b&field=c");
        }
    }
}
