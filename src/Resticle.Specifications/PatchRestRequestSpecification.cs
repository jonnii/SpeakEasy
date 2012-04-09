using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace HttpSpeak.Specifications
{
    public class PatchRestRequestSpecification
    {
        [Subject(typeof(PutRestRequest))]
        public class when_building_web_request : with_patch_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(serializer);

            It should_have_put_method = () =>
                webRequest.Method.ShouldEqual("PATCH");

            static WebRequest webRequest;
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                serializer = An<ITransmissionSettings>();

            protected static ITransmissionSettings serializer;
        }

        public class with_patch_request : with_serializer
        {
            Establish context = () =>
                request = new PatchRestRequest(new Resource("http://example.com/companies"), null);

            protected static PatchRestRequest request;
        }
    }
}