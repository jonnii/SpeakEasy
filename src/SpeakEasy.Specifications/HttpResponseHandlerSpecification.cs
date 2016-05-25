using System;
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
            Establish context = () =>
            {
                The<IHttpResponse>()
                    .WhenToldTo(r => r.ConsumeBody(Param.IsAny<Func<Stream, Company>>()))
                    .Return(new Func<Func<Stream, Company>, Company>(f => f(new MemoryStream(Encoding.Default.GetBytes("abcd")))));

                deserializer = An<ISerializer>();

                The<IHttpResponse>().WhenToldTo(r => r.Deserializer).Return(deserializer);
            };

            Because of = () =>
                Subject.As<Company>();

            It should_deserialize_with_deserializer = () =>
                deserializer.WasToldTo(d => d.Deserialize<Company>(Param.IsAny<Stream>()));

            static ISerializer deserializer;
        }

        class when_getting_as_byte_array
        {
            Establish context = () =>
                The<IHttpResponse>()
                    .WhenToldTo(r => r.ConsumeBody(Param.IsAny<Func<Stream, byte[]>>()))
                    .Return(new Func<Func<Stream, byte[]>, byte[]>(f => f(new MemoryStream(Encoding.Default.GetBytes("abcd")))));

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
