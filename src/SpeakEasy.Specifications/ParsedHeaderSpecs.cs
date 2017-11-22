using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(ParsedHeaderValue))]
    class ParsedHeaderSpecs
    {
        class when_getting_unknown_parameter
        {
            static ParsedHeaderValue headerValue;

            static Exception exception;

            Establish context = () =>
                headerValue = new ParsedHeaderValue(new Dictionary<string, ParsedHeaderParameter[]> { { "parameter", new[] { new ParsedHeaderParameter("name", "value") } } });

            Because of = () =>
                exception = Catch.Exception(() => headerValue.GetParameter("unknown", "fribble"));

            It should_throw_exception = () =>
                exception.ShouldBeOfExactType<ArgumentException>();
        }
    }
}
