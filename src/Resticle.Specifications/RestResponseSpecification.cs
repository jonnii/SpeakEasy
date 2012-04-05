using System;
using System.Net;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestResponseSpecification
    {
        [Subject(typeof(RestResponse))]
        public class when_on_with_correct_status_code_and_callback
        {
            Establish context = () =>
                response = new RestResponse(new Uri("http://example.com/companies"), HttpStatusCode.OK);

            Because of = () =>
                response.On(HttpStatusCode.OK, () => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static RestResponse response;

            static bool called;
        }

        [Subject(typeof(RestResponse))]
        public class when_on_with_incorrect_status_code_and_callback
        {
            Establish context = () =>
                response = new RestResponse(new Uri("http://example.com/companies"), HttpStatusCode.OK);

            Because of = () =>
                response.On(HttpStatusCode.Created, () => { called = true; });

            It should_not_call_callback = () =>
                called.ShouldBeFalse();

            static RestResponse response;

            static bool called;
        }

        [Subject(typeof(RestResponse))]
        public class when_on_ok_with_callback
        {
            Establish context = () =>
                response = new RestResponse(new Uri("http://example.com/companies"), HttpStatusCode.OK);

            Because of = () =>
                response.OnOk(() => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static RestResponse response;

            static bool called;
        }
    }
}
