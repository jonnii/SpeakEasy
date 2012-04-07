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
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_delete_method = () =>
                webRequest.Method.ShouldEqual("HEAD");

            static WebRequest webRequest;
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                transmissionSettings = An<ITransmissionSettings>();

            protected static ITransmissionSettings transmissionSettings;
        }

        public class with_head_request : with_serializer
        {
            Establish context = () =>
                request = new HeadRestRequest(new Resource("http://example.com/companies"));

            protected static HeadRestRequest request;
        }
    }
}