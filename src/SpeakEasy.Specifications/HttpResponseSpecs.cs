using System;
using System.IO;
using System.Net;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Specifications.Fixtures;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(HttpResponse))]
    class HttpResponseSpecs : WithFakes
    {
        static ISerializer deserializer;

        static Stream bodyStream;

        static Exception exception;

        static bool called;

        Establish context = () =>
        {
            deserializer = An<ISerializer>();
            bodyStream = new MemoryStream(Encoding.UTF8.GetBytes("lollipops"));
            called = false;
        };

        class with_ok_response
        {
            static HttpResponse response;

            Establish context = () =>
                response = HttpResponses.Create(deserializer, bodyStream, HttpStatusCode.OK);

            class in_general_with_ok_response
            {
                It should_indicate_when_ok = () =>
                    response.IsOk().ShouldBeTrue();

                It should_indicate_is_not_other_status_code = () =>
                    response.Is(HttpStatusCode.BadRequest).ShouldBeFalse();
            }

            class when_on_with_correct_status_and_typed_callback
            {
                Because of = () =>
                    response.On(HttpStatusCode.OK, (Person p) => { called = true; });

                It should_deserialize_person = () =>
                    deserializer.WasToldTo(d => d.Deserialize<Person>(Param.IsAny<Stream>()));

                It should_call_callback = () =>
                    called.ShouldBeTrue();
            }

            class when_on_with_incorrect_status_and_typed_callback
            {
                Because of = () =>
                    response.On(HttpStatusCode.Created, (Person p) => { called = true; });

                It should_call_callback = () =>
                    called.ShouldBeFalse();
            }

            class when_on_ok_with_correct_status_and_typed_callback
            {
                Because of = () =>
                    response.OnOk((Person p) => { called = true; });

                It should_deserialize_person = () =>
                    deserializer.WasToldTo(d => d.Deserialize<Person>(Param.IsAny<Stream>()));

                It should_call_callback = () =>
                    called.ShouldBeTrue();
            }

            class when_on_ok_with_correct_status_code
            {
                static IHttpResponseHandler handler;

                Because of = () =>
                    handler = response.OnOk();

                It should_return_handler = () =>
                    handler.ShouldNotBeNull();
            }

            class when_on_with_correct_status_code_and_callback
            {
                Because of = () =>
                    response.On(HttpStatusCode.OK, () => { called = true; });

                It should_call_callback = () =>
                    called.ShouldBeTrue();
            }

            class when_on_with_incorrect_status_code_and_callback
            {
                Because of = () =>
                    response.On(HttpStatusCode.Created, () => { called = true; });

                It should_not_call_callback = () =>
                    called.ShouldBeFalse();
            }

            class when_on_ok_with_callback
            {
                Because of = () =>
                    response.OnOk(() => { called = true; });

                It should_call_callback = () =>
                    called.ShouldBeTrue();
            }

            class when_on_with_state_callback
            {
                Because of = () =>
                    response.On(HttpStatusCode.OK, state => { called = true; });

                It should_call_callback = () =>
                    called.ShouldBeTrue();
            }

            //class when_getting_header
            //{
            //    Because of = () =>
            //        header = response.GetHeaderValue("awesome-header");

            //    It should_get_header_value = () =>
            //        header.ShouldEqual("value");

            //    static string header;
            //}

            //class when_getting_header_with_different_case
            //{
            //    Because of = () =>
            //        header = response.GetHeaderValue("AWESOME-HEADER");

            //    It should_get_header_value = () =>
            //        header.ShouldEqual("value");

            //    static string header;
            //}

            //class when_getting_non_existant_header
            //{
            //    Because of = () =>
            //        header = Catch.Exception(() => response.GetHeaderValue("fribble"));

            //    It should_throw_exception = () =>
            //        header.ShouldBeOfExactType<ArgumentException>();

            //    static Exception header;
            //}
        }

        class with_created_response
        {
            static HttpResponse response;

            Establish context = () =>
                response = HttpResponses.Create(deserializer, bodyStream, HttpStatusCode.Created);

            class when_on_ok_with_incorrect_status_code
            {
                Because of = () =>
                    exception = Catch.Exception(() => response.OnOk());

                It should_return_handler = () =>
                    exception.ShouldBeOfExactType<HttpException>();
            }

            class when_on_with_incorrect_status_code
            {
                Because of = () =>
                    exception = Catch.Exception(() => response.On(HttpStatusCode.OK));

                It should_throw_http_exception = () =>
                    exception.ShouldBeOfExactType<HttpException>();
            }

            class when_on_with_incorrect_integer_status_code
            {
                Because of = () =>
                    exception = Catch.Exception(() => response.On(301));

                It should_throw_http_exception = () =>
                    exception.ShouldBeOfExactType<HttpException>();
            }
        }

        class Person
        {
        }
    }
}
