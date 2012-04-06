using System.Net;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class DeleteRestRequestSpecification
    {
        [Subject(typeof(DeleteRestRequest))]
        public class when_building_web_request : with_delete_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest();

            It should_have_delete_method = () =>
                webRequest.Method.ShouldEqual("DELETE");

            static WebRequest webRequest;
        }

        public class with_delete_request
        {
            Establish context = () =>
                request = new DeleteRestRequest("http://example.com/companies");

            protected static DeleteRestRequest request;
        }
    }
}