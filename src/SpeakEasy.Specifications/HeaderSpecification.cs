using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class HeaderSpecification
    {
        [Subject(typeof(Header))]
        public class when_parsing_simple_header
        {
            Establish context = () =>
                header = new Header("Content-Type", "application/json");

            Because of = () =>
                parsed = header.Parse();

            It should_have_same_name_as_original_header = () =>
                parsed.Name.ShouldEqual("Content-Type");

            It should_have_same_value_as_original_header = () =>
                parsed.Value.ShouldEqual("application/json");

            static Header header;

            static ParsedHeader parsed;
        }

        [Subject(typeof(Header))]
        public class when_parsing_header_with_parameters
        {
            Establish context = () =>
                header = new Header("Content-Disposition", "attachment; filename=foo.txt; name=fribble");

            Because of = () =>
                parsed = header.Parse();

            It should_have_filename_parameter = () =>
                parsed.GetParameter("filename").ShouldEqual("foo.txt");

            It should_have_name_parameter = () =>
                parsed.GetParameter("name").ShouldEqual("fribble");

            It should_have_key = () =>
                parsed.Value.ShouldEqual("attachment");

            static Header header;

            static ParsedHeader parsed;
        }
    }

    public class ParsedHeaderSpecification
    {
        [Subject(typeof(ParsedHeader))]
        public class when_getting_unknown_parameter
        {
            Establish context = () =>
                header = new ParsedHeader("name", "raw value", new Dictionary<string, string> { { "parameter", "value" } });

            Because of = () =>
                exception = Catch.Exception(() => header.GetParameter("unknown"));

            It should_throw_exception = () =>
                exception.ShouldBeOfType<ArgumentException>();

            static ParsedHeader header;

            static Exception exception;
        }
    }
}
