using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HeadRequest))]
    class HeadRequestSpecs : WithFakes
    {
        static HeadRequest request;

        Establish context = () =>
            request = new HeadRequest(Resource.Create("http://example.com/companies"));

        class when_building_web_request
        {
            It should_have_head_method = () =>
                request.HttpMethod.ShouldEqual(HttpMethod.Head);
        }
    }
}
