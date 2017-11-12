using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(PutRequest))]
    class PutRequestSpecification : WithFakes
    {
        static ITransmissionSettings serializer;

        Establish context = () =>
            serializer = An<ITransmissionSettings>();

        class with_put_request
        {
            static PutRequest request;

            Establish context = () =>
                request = new PutRequest(new Resource("http://example.com/companies"));

            It should_have_put_method = () =>
                request.HttpMethod.ShouldEqual(HttpMethod.Put);
        }
    }
}
