// using System.Net;
// using Machine.Fakes;
// using Machine.Specifications;

// namespace SpeakEasy.Specifications
// {
//     public class HeadRequestSpecification
//     {
//         [Subject(typeof(HeadRequest))]
//         public class when_building_web_request : with_head_request
//         {
//             It should_have_delete_method = () =>
//                 request.HttpMethod.ShouldEqual("HEAD");

//             static WebRequest webRequest;
//         }

//         public class with_serializer : WithFakes
//         {
//             Establish context = () =>
//                 transmissionSettings = An<ITransmissionSettings>();

//             protected static ITransmissionSettings transmissionSettings;
//         }

//         public class with_head_request : with_serializer
//         {
//             Establish context = () =>
//                 request = new HeadRequest(new Resource("http://example.com/companies"));

//             internal static HeadRequest request;
//         }
//     }
// }