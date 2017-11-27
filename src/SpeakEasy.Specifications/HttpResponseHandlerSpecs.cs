using System.IO;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpResponseHandler))]
    class HttpResponseHandlerSpecs : WithFakes
    {
        static HttpResponseHandler handler;

        Establish context = () =>
            handler = new HttpResponseHandler(The<IHttpResponse>(), The<ISerializer>(), new SingleUseStream(new MemoryStream(Encoding.UTF8.GetBytes("abcd"))));

        class when_unwrapping_as
        {
            Because of = () =>
                handler.As<Company>();

            It should_deserialize_with_deserializer = () =>
                The<ISerializer>().WasToldTo(d => d.Deserialize<Company>(Param.IsAny<Stream>()));
        }

        class when_getting_as_byte_array
        {
            static byte[] bytes;

            Because of = () =>
                bytes = handler.AsByteArray().Await();

            It should_get_bytes = () =>
                bytes.Length.ShouldBeGreaterThan(0);
        }

        public class Company
        {
        }
    }
}
