using System.Net.Http;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(DeleteRequest))]
    class DeleteRequestSpecification
    {
        static DeleteRequest request;

        class in_general
        {
            Establish context = () =>
                request = new DeleteRequest(new Resource("hello"));

            It should_have_delete_method = () =>
                request.HttpMethod.ShouldEqual(HttpMethod.Delete);
        }
    }
}
