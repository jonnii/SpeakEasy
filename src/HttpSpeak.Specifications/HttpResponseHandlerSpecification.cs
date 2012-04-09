using Machine.Fakes;
using Machine.Specifications;

namespace HttpSpeak.Specifications
{
    public class HttpResponseHandlerSpecification
    {
        [Subject(typeof(HttpResponseHandler))]
        public class when_unwrapping : WithSubject<HttpResponseHandler>
        {
            Establish context = () =>
            {
                deserializer = An<ISerializer>();

                The<IHttpResponse>().WhenToldTo(r => r.Deserializer).Return(deserializer);
            };

            Because of = () =>
                Subject.Unwrap<Company>();

            It should_deserialize_with_deserializer = () =>
                deserializer.WasToldTo(d => d.Deserialize<Company>(Param.IsAny<string>()));

            static ISerializer deserializer;
        }

        public class Company
        {
        }
    }
}
