using System.Net.Http;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(OptionsRequest))]
    class OptionsRequestSpecs
    {
        class with_options_request
        {
            static OptionsRequest request;

            Establish context = () =>
                request = new OptionsRequest(new Resource("http://example.com/companies"));

            class when_building_web_request
            {
                It should_have_options_method = () =>
                    request.HttpMethod.ShouldEqual(HttpMethod.Options);
            }
        }
    }
}
