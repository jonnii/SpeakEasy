using System.IO;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class HttpResponseHandlerSpecification
    {
        [Subject(typeof(HttpResponseHandler))]
        public class when_unwrapping_as : WithSubject<HttpResponseHandler>
        {
            Establish context = () =>
            {
                deserializer = An<ISerializer>();

                The<IHttpResponse>().WhenToldTo(r => r.Deserializer).Return(deserializer);
            };

            Because of = () =>
                Subject.As<Company>();

            It should_deserialize_with_deserializer = () =>
                deserializer.WasToldTo(d => d.Deserialize<Company>(Param.IsAny<Stream>()));

            static ISerializer deserializer;
        }

        public class Company
        {
        }
    }
}
