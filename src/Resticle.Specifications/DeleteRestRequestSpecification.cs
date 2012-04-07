using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class DeleteRestRequestSpecification
    {
        [Subject(typeof(DeleteRestRequest))]
        public class when_building_web_request : with_delete_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_delete_method = () =>
                webRequest.Method.ShouldEqual("DELETE");

            static WebRequest webRequest;
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                transmissionSettings = An<ITransmissionSettings>();

            protected static ITransmissionSettings transmissionSettings;
        }

        public class with_delete_request : with_serializer
        {
            Establish context = () =>
                request = new DeleteRestRequest(new Resource("http://example.com/companies"));

            protected static DeleteRestRequest request;
        }
    }
}