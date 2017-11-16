using System.Net;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Requests;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(PatchRequest))]
    class PatchRequestSpecification
    {
        //public class when_building_web_request : with_patch_request
        //{
        //    It should_have_patch_method = () =>
        //        request.HttpMethod.ShouldEqual("PATCH");

        //    static WebRequest webRequest;
        //}

        //public class with_serializer : WithFakes
        //{
        //    Establish context = () =>
        //        serializer = An<ITransmissionSettings>();

        //    protected static ITransmissionSettings serializer;
        //}

        //public class with_patch_request : with_serializer
        //{
        //    Establish context = () =>
        //        request = new PatchRequest(new Resource("http://example.com/companies"));

        //    internal static PatchRequest request;
        //}
    }
}
