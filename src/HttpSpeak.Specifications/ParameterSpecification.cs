using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class ParameterSpecification
    {
        [Subject(typeof(Parameter))]
        public class when_converting_to_query_string
        {
            Establish context = () =>
                parameter = new Parameter("name", "value");

            Because of = () =>
                formatted = parameter.ToQueryString();

            It should_format_as_query_string = () =>
                formatted.ShouldEqual("name=value");

            static Parameter parameter;

            static string formatted;
        }
    }
}