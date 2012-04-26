using System;
using System.IO;
using System.Net;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Specifications.Fixtures;

namespace SpeakEasy.Specifications
{
    public class HttpResponseSpecification
    {
        [Subject(typeof(HttpResponse))]
        public class in_general_with_ok_response : with_ok_response
        {
            It should_indicate_when_ok = () =>
                response.IsOk().ShouldBeTrue();

            It should_indicate_is_not_other_status_code = () =>
                response.Is(HttpStatusCode.BadRequest).ShouldBeFalse();
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_with_correct_status_and_typed_callback : with_ok_response
        {
            Because of = () =>
                response.On(HttpStatusCode.OK, (Person p) => { called = true; });

            It should_deserialize_person = () =>
                deserializer.WasToldTo(d => d.Deserialize<Person>(Param.IsAny<Stream>()));

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static bool called;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_with_incorrect_status_and_typed_callback : with_ok_response
        {
            Because of = () =>
                response.On(HttpStatusCode.Created, (Person p) => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeFalse();

            static bool called;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_ok_with_correct_status_and_typed_callback : with_ok_response
        {
            Because of = () =>
                response.OnOk((Person p) => { called = true; });

            It should_deserialize_person = () =>
                deserializer.WasToldTo(d => d.Deserialize<Person>(Param.IsAny<Stream>()));

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static bool called;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_ok_with_correct_status_code : with_ok_response
        {
            Because of = () =>
                handler = response.OnOk();

            It should_return_handler = () =>
                handler.ShouldNotBeNull();

            static IHttpResponseHandler handler;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_ok_with_incorrect_status_code : with_created_response
        {
            Because of = () =>
                exception = Catch.Exception(() => response.OnOk());

            It should_return_handler = () =>
                exception.ShouldBeOfType<HttpException>();

            static Exception exception;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_with_correct_status_code_and_callback : with_ok_response
        {
            Because of = () =>
                response.On(HttpStatusCode.OK, () => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static bool called;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_with_incorrect_status_code_and_callback : with_ok_response
        {
            Because of = () =>
                response.On(HttpStatusCode.Created, () => { called = true; });

            It should_not_call_callback = () =>
                called.ShouldBeFalse();

            static bool called;
        }

        [Subject(typeof(HttpResponse))]
        public class when_on_ok_with_callback : with_ok_response
        {
            Because of = () =>
                response.OnOk(() => { called = true; });

            It should_call_callback = () =>
                called.ShouldBeTrue();

            static bool called;
        }

        [Subject(typeof(HttpResponse))]
        public class when_getting_header : with_ok_response
        {
            Because of = () =>
                header = response.GetHeaderValue("awesome-header");

            It should_get_header_value = () =>
                header.ShouldEqual("value");

            static string header;
        }

        [Subject(typeof(HttpResponse))]
        public class when_getting_header_with_different_case : with_ok_response
        {
            Because of = () =>
                header = response.GetHeaderValue("AWESOME-HEADER");

            It should_get_header_value = () =>
                header.ShouldEqual("value");

            static string header;
        }

        [Subject(typeof(HttpResponse))]
        public class when_getting_non_existant_header : with_ok_response
        {
            Because of = () =>
                header = Catch.Exception(() => response.GetHeaderValue("fribble"));

            It should_throw_exception = () =>
                header.ShouldBeOfType<ArgumentException>();

            static Exception header;
        }

        public class with_deserializer : WithFakes
        {
            Establish context = () =>
            {
                deserializer = An<ISerializer>();
                bodyStream = new MemoryStream(Encoding.Default.GetBytes("lollipops"));
            };

            protected static ISerializer deserializer;

            protected static Stream bodyStream;
        }

        public class with_ok_response : with_deserializer
        {
            Establish context = () =>
                response = HttpResponses.Create(deserializer, bodyStream, HttpStatusCode.OK);

            protected static HttpResponse response;
        }

        public class with_created_response : with_deserializer
        {
            Establish context = () =>
                response = HttpResponses.Create(deserializer, bodyStream, HttpStatusCode.Created);

            protected static HttpResponse response;
        }

        public class Person { }
    }
}
