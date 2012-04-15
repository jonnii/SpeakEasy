using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class ParsedHeaderSpecification
    {
        [Subject(typeof(ParsedHeaderValue))]
        public class when_getting_unknown_parameter
        {
            Establish context = () =>
                headerValue = new ParsedHeaderValue(new Dictionary<string, ParsedHeaderParameter[]> { { "parameter", new[] { new ParsedHeaderParameter("name", "value") } } });

            Because of = () =>
                exception = Catch.Exception(() => headerValue.GetParameter("unknown", "fribble"));

            It should_throw_exception = () =>
                exception.ShouldBeOfType<ArgumentException>();

            static ParsedHeaderValue headerValue;

            static Exception exception;
        }
    }
}