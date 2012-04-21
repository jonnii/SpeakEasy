using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class PostRequestSpecification
    {
        [Subject(typeof(PostRequest))]
        public class in_general_without_body
        {
            Establish context = () =>
                request = new PostRequest(new Resource("http://example.com/companies"));

            It should_have_null_body = () =>
                request.Body.ShouldBeOfType<PostRequestBody>();

            static PostRequest request;
        }

        public class with_transmission : WithFakes
        {
            Establish context = () =>
            {
                transmissionSettings = An<ITransmissionSettings>();
                transmissionSettings.WhenToldTo(r => r.DefaultSerializerContentType).Return("application/json");
            };

            protected static ITransmissionSettings transmissionSettings;
        }
    }
}
