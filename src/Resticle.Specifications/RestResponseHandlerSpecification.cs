using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestResponseHandlerSpecification
    {
        [Subject(typeof(RestResponseHandler))]
        public class when_unwrapping : WithSubject<RestResponseHandler>
        {
            Because of = () =>
                Subject.Unwrap<Company>();

            It should_deserialize_with_deserializer = () =>
                The<IDeserializer>().WasToldTo(d => d.Deserialize<Company>(Param.IsAny<string>()));
        }

        public class Company
        {
        }
    }
}
