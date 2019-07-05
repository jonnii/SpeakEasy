using System;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(Parameter))]
    class ParameterSpecs
    {
        static Parameter parameter;

        class when_value_is_nullable
        {
            Establish context = () =>
                parameter = new Parameter("name", new DateTime?());

            It should_not_have_value = () =>
                parameter.HasValue.ShouldBeFalse();
        }
    }
}
