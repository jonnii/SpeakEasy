using System.IO;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpResponseHandler))]
    class HttpResponseHandlerSpecification : WithSubject<HttpResponseHandler>
    {
        class when_unwrapping_as
        {
            static ISerializer deserializer;

            Establish context = () =>
            {
                deserializer = An<ISerializer>();

                The<IHttpResponseWithBody>().WhenToldTo(r => r.Deserializer).Return(deserializer);
            };

            Because of = () =>
                Subject.As<Company>();

            It should_deserialize_with_deserializer = () =>
                deserializer.WasToldTo(d => d.Deserialize<Company>(Param.IsAny<Stream>()));
        }

        class when_getting_as_byte_array
        {
            static byte[] bytes;

            Establish context = () =>
                The<IHttpResponseWithBody>().WhenToldTo(r => r.Body).Return(new MemoryStream(Encoding.UTF8.GetBytes("abcd")));

            Because of = () =>
                bytes = Subject.AsByteArray().Await();

            It should_get_bytes = () =>
                bytes.Length.ShouldBeGreaterThan(0);
        }

        public class Company
        {
        }
    }
}
