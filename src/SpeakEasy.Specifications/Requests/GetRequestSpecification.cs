using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    public class GetRequestSpecification
    {
        [Subject(typeof(GetRequest))]
        public class when_building_web_request_with_parameters : with_transmission
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("filter", "ftse");
                resource.AddParameter("starred", true);

                request = new GetRequest(resource);
            };

            Because of = () =>
                url = request.BuildRequestUrl(An<IArrayFormatter>());

            It should_set_url = () =>
                url.ShouldEqual("http://example.com/companies?filter=ftse&starred=True");

            static string url;

            static GetRequest request;
        }

        public class with_transmission : WithFakes
        {
            Establish context = () =>
                transmissionSettings = An<ITransmissionSettings>();

            protected static ITransmissionSettings transmissionSettings;
        }

        public class with_get_request : with_transmission
        {
            Establish context = () =>
                request = new GetRequest(new Resource("http://example.com/companies"));

            internal static GetRequest request;
        }
    }
}
