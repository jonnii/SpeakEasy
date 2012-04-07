using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class OptionsRestRequestSpecification
    {
        [Subject(typeof(OptionsRestRequest))]
        public class when_building_web_request : with_options_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmissionSettings);

            It should_have_options_method = () =>
                webRequest.Method.ShouldEqual("OPTIONS");

            static WebRequest webRequest;
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                transmissionSettings = An<ITransmissionSettings>();

            protected static ITransmissionSettings transmissionSettings;
        }

        public class with_options_request : with_serializer
        {
            Establish context = () =>
                request = new OptionsRestRequest(new Resource("http://example.com/companies"));

            protected static OptionsRestRequest request;
        }
    }
}