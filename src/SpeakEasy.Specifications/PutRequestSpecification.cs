using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class PutRequestSpecification
    {
        [Subject(typeof(PutRequest))]
        public class when_building_web_request : with_put_request
        {
            It should_have_put_method = () =>
                request.HttpMethod.ShouldEqual("PUT");
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                serializer = An<ITransmissionSettings>();

            protected static ITransmissionSettings serializer;
        }

        public class with_put_request : with_serializer
        {
            Establish context = () =>
                request = new PutRequest(new Resource("http://example.com/companies"));

            protected static PutRequest request;
        }
    }
}