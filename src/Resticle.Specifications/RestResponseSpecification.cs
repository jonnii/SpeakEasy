using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class RestResponseSpecification
    {
        [Subject(typeof(RestResponse))]
        public class in_general_with_ok_response : with_ok_response
        {
            It should_indicate_when_ok = () =>
                response.IsOk().ShouldBeTrue();

            It should_indicate_is_not_other_status_code = () =>
                response.Is(HttpStatusCode.BadRequest).ShouldBeFalse();
        }

        [Subject(typeof(RestResponse))]
        public class when_on_ok_with_correct_status_code : with_ok_response
        {
            Because of = () =>
                handler = response.OnOk();

            It should_return_handler = () =>
                handler.ShouldNotBeNull();

            static IRestResponseHandler handler;
        }

        [Subject(typeof(RestResponse))]
        public class when_on_ok_with_incorrect_status_code : with_created_response
        {
            Because of = () =>
                exception = Catch.Exception(() => response.OnOk());

            It should_return_handler = () =>
                exception.ShouldBeOfType<RestException>();

            static Exception exception;
        }

        [Subject(typeof(RestResponse))]
        public class when_on_with_correct_status_code_and_callback : with_ok_response
        {
            Because of = () =>
                response.On(HttpStatusCode.OK, () => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static bool called;
        }

        [Subject(typeof(RestResponse))]
        public class when_on_with_incorrect_status_code_and_callback : with_ok_response
        {
            Because of = () =>
                response.On(HttpStatusCode.Created, () => { called = true; });

            It should_not_call_callback = () =>
                called.ShouldBeFalse();

            static bool called;
        }

        [Subject(typeof(RestResponse))]
        public class when_on_ok_with_callback : with_ok_response
        {
            Because of = () =>
                response.OnOk(() => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static bool called;
        }

        public class with_deserializer : WithFakes
        {
            Establish context = () =>
                deserializer = An<IDeserializer>();

            protected static IDeserializer deserializer;
        }

        public class with_ok_response : with_deserializer
        {
            Establish context = () =>
                response = new RestResponse(deserializer, new Uri("http://example.com/companies"), HttpStatusCode.OK, "body");

            protected static RestResponse response;
        }

        public class with_created_response : with_deserializer
        {
            Establish context = () =>
                response = new RestResponse(deserializer, new Uri("http://example.com/companies"), HttpStatusCode.Created, "body");

            protected static RestResponse response;
        }
    }
}
