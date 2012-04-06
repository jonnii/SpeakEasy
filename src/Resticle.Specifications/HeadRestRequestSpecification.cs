using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class HeadRestRequestSpecification
    {
        [Subject(typeof(DeleteRestRequest))]
        public class when_building_web_request : with_head_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmission);

            It should_have_delete_method = () =>
                webRequest.Method.ShouldEqual("HEAD");

            static WebRequest webRequest;
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                transmission = An<ITransmission>();

            protected static ITransmission transmission;
        }

        public class with_head_request : with_serializer
        {
            Establish context = () =>
                request = new HeadRestRequest("http://example.com/companies");

            protected static HeadRestRequest request;
        }
    }
}