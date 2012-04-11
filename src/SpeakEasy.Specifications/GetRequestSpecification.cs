using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class GetRequestSpecification
    {
        [Subject(typeof(GetRequest))]
        public class when_building_web_request : with_get_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_set_url = () =>
                request.Resource.Path.ShouldEqual("http://example.com/companies");

            It should_set_request_to_get_request = () =>
                webRequest.Method.ShouldEqual("GET");

            static WebRequest webRequest;
        }

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
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_set_url = () =>
                webRequest.RequestUri.ToString().ShouldEqual("http://example.com/companies?filter=ftse&starred=True");

            static WebRequest webRequest;

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

            protected static GetRequest request;
        }
    }
}
