using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class DeleteRequestSpecification
    {
        [Subject(typeof(DeleteRequest))]
        public class in_general : with_delete_request
        {
            It should_have_delete_method = () =>
                request.HttpMethod.ShouldEqual("DELETE");

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
                request = new DeleteRequest(new Resource("http://example.com/companies"));

            protected static DeleteRequest request;
        }
    }
}