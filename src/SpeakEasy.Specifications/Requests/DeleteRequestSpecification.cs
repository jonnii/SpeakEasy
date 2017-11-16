using System.Net.Http;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(DeleteRequest))]
    class DeleteRequestSpecification : WithFakes
    {
        static ITransmissionSettings transmissionSettings;

        static DeleteRequest request;

        class in_general
        {
            Establish context = () =>
            {
                transmissionSettings = An<ITransmissionSettings>();
                request = new DeleteRequest(new Resource("http://example.com/companies"));
            };

            It should_have_delete_method = () =>
                request.HttpMethod.ShouldEqual(HttpMethod.Delete);
        }
    }
}