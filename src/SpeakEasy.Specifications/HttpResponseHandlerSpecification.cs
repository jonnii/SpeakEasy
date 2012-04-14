using System.IO;
using System.Text;
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

        [Subject(typeof(HttpResponseHandler))]
        public class when_getting_as_byte_array : WithSubject<HttpResponseHandler>
        {
            Establish context = () =>
                The<IHttpResponse>().WhenToldTo(r => r.Body).Return(new MemoryStream(Encoding.Default.GetBytes("abcd")));

            Because of = () =>
                bytes = Subject.AsByteArray();

            It should_get_bytes = () =>
                bytes.Length.ShouldBeGreaterThan(0);

            static byte[] bytes;
        }

        public class Company
        {
        }
    }
}
