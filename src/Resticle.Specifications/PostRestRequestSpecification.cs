using System;
using System.Net;

using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class PostRestRequestSpecification
    {
        [Subject(typeof(PostRestRequest))]
        public class when_building_web_request : with_post_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmission);

            It should_have_post_method = () =>
                webRequest.Method.ShouldEqual("POST");

            static WebRequest webRequest;
        }

        public class with_serializer : WithFakes
        {
            Establish context = () =>
                transmission = An<ITransmission>();

            protected static ITransmission transmission;
        }

        public class with_post_request : with_serializer
        {
            Establish context = () =>
                request = new PostRestRequest("http://example.com/companies", null);

            protected static PostRestRequest request;
        }
    }
}
