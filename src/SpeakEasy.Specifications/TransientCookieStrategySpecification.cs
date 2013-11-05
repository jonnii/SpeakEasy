using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class TransientCookieStrategySpecification
    {
        [Subject(typeof(TransientCookieStrategy))]
        public class when_getting_cookie : WithSubject<TransientCookieStrategy>
        {
            Because of = () =>
                container = Subject.Get(An<IHttpRequest>());

            It should_get_different_cookie_container_on_each_request = () =>
                Subject.Get(An<IHttpRequest>()).ShouldNotBeTheSameAs(container);

            static CookieContainer container;
        }
    }
}
