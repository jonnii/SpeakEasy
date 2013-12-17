using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class MultipleValuesArrayFormatterSpecification
    {
        [Subject(typeof(MultipleValuesArrayFormatter))]
        public class when_formatting : WithSubject<MultipleValuesArrayFormatter>
        {
            Because of = () =>
                formatted = Subject.FormatParameter("field", new[] { "a", "b", "c" }, t => t.ToString());

            It should_format_each_item = () =>
                formatted.ShouldEqual("field=a&field=b&field=c");

            static string formatted;
        }
    }
}
