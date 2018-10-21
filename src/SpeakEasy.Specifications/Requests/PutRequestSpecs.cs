using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(PutRequest))]
    class PutRequestSpecs : WithFakes
    {
        static ITransmissionSettings serializer;

        Establish context = () =>
            serializer = An<ITransmissionSettings>();

        class with_put_request
        {
            static PutRequest request;

            Establish context = () =>
                request = new PutRequest(Resource.Create("http://example.com/companies"));

            It should_have_put_method = () =>
                request.HttpMethod.ShouldEqual(HttpMethod.Put);
        }
    }
}
