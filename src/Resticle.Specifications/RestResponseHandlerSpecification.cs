using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestResponseHandlerSpecification
    {
        [Subject(typeof(RestResponseHandler))]
        public class when_unwrapping : WithSubject<RestResponseHandler>
        {
            Establish context = () =>
            {
                deserializer = An<IDeserializer>();

                The<IRestResponse>().WhenToldTo(r => r.Deserializer).Return(deserializer);
            };

            Because of = () =>
                Subject.Unwrap<Company>();

            It should_deserialize_with_deserializer = () =>
                deserializer.WasToldTo(d => d.Deserialize<Company>(Param.IsAny<string>()));

            static IDeserializer deserializer;
        }

        public class Company
        {
        }
    }
}
