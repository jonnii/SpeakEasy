using Machine.Specifications;
using SpeakEasy.Requests;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(GetRequest))]
    class GetRequestSpecs
    {
        class with_get_request
        {
            static GetRequest request;

            Establish context = () =>
                request = new GetRequest(Resource.Create("http://example.com/companies"));

            class when_building_web_request_with_parameters
            {
                static string url;

                static GetRequest request;

                Establish context = () =>
                {
                    var resource = Resource.Create("http://example.com/companies");
                    resource.AddParameter("filter", "ftse");
                    resource.AddParameter("starred", true);

                    request = new GetRequest(resource);
                };

                Because of = () =>
                    url = request.BuildRequestUrl(new DefaultQuerySerializer());

                It should_set_url = () =>
                    url.ShouldEqual("http://example.com/companies?filter=ftse&starred=True");
            }
        }
    }
}
