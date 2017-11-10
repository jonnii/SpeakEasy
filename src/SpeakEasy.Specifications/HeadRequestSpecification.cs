using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HeadRequest))]
    class HeadRequestSpecification : WithFakes
    {
        static HeadRequest request;
        
        Establish context = () =>
            request = new HeadRequest(new Resource("http://example.com/companies"));
    
        class when_building_web_request
        {
            It should_have_head_method = () =>
                request.HttpMethod.ShouldEqual("HEAD");
        }
    }
}