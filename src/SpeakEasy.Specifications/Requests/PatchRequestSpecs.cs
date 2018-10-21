using System.Net.Http;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(PatchRequest))]
    class PatchRequestSpecs
    {
        class with_patch_request
        {
            static PatchRequest request;

            Establish context = () =>
                request = new PatchRequest(Resource.Create("http://example.com/companies"));

            class when_building_web_request
            {
                It should_have_patch_method = () =>
                    request.HttpMethod.ShouldEqual(new HttpMethod("PATCH"));
            }
        }
    }
}
