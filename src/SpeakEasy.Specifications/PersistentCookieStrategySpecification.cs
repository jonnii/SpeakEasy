using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class PersistentCookieStrategySpecification
    {
        [Subject(typeof(PersistentCookieStrategy))]
        public class when_getting_cookie : with_cookie_container
        {
            Because of = () =>
                container = Subject.Get(An<IHttpRequest>());

            It should_get_same_cookie_container_on_each_request = () =>
                Subject.Get(An<IHttpRequest>()).ShouldBeTheSameAs(container);

            static CookieContainer container;
        }

        public class with_cookie_container : WithFakes
        {
            Establish context = () =>
                Subject = new PersistentCookieStrategy();

            protected static PersistentCookieStrategy Subject;
        }
    }
}