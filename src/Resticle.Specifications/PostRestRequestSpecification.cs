using System;
using System.Net;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class PostRestRequestSpecification
    {
        [Subject(typeof(PostRestRequest))]
        public class when_building_web_request : with_post_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest();

            It should_have_post_method = () =>
                webRequest.Method.ShouldEqual("POST");

            static WebRequest webRequest;
        }

        public class with_post_request
        {
            Establish context = () =>
                request = new PostRestRequest(new Uri("http://example.com/companies"));

            protected static PostRestRequest request;
        }
    }
}
