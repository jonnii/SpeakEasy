using System.Linq;
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
                parsed = header.ParseValue();

            It should_have_single_key = () =>
                parsed.Keys.Count().ShouldEqual(1);

            It should_have_value_as_keyk = () =>
                parsed.Keys.ElementAt(0).ShouldEqual("application/json");

            static Header header;

            static ParsedHeaderValue parsed;
        }

        [Subject(typeof(Header))]
        public class when_parsing_header_with_parameters
        {
            Establish context = () =>
                header = new Header("Content-Disposition", "attachment; filename=foo.txt; name=fribble");

            Because of = () =>
                parsed = header.ParseValue();

            It should_have_key_for_attachment = () =>
                parsed.Keys.Single().ShouldEqual("attachment");

            It should_get_parameters_for_key = () =>
                parsed.GetParameters("attachment").Count().ShouldEqual(2);

            It should_have_filename_parameter = () =>
                parsed.GetParameter("attachment", "filename").ShouldEqual("foo.txt");

            It should_have_name_parameter = () =>
                parsed.GetParameter("attachment", "name").ShouldEqual("fribble");

            static Header header;

            static ParsedHeaderValue parsed;
        }

        [Subject(typeof(Header))]
        public class when_parsing_link_header
        {
            Establish context = () =>
                header = new Header("Link", "<https://api.github.com/gists?page=2>; rel=\"next\", <https://api.github.com/gists?page=22817>; rel=\"last\"");

            Because of = () =>
                parsed = header.ParseValue();

            It should_have_keys_for_each_url = () =>
            {
                parsed.Keys.First().ShouldEqual("<https://api.github.com/gists?page=2>");
                parsed.Keys.Last().ShouldEqual("<https://api.github.com/gists?page=22817>");
            };

            It should_have_parameter_for_url = () =>
                parsed.GetParameters("<https://api.github.com/gists?page=2>").First().Name.ShouldEqual("rel");

            static Header header;

            static ParsedHeaderValue parsed;
        }
    }
}
