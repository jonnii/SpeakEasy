using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Bodies;
using SpeakEasy.Requests;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications.Requests
{
    [Subject(typeof(PostRequest))]
    class PostRequestSpecs : WithFakes
    {
        static PostRequest request;

        class in_general_without_body
        {
            Establish context = () =>
                request = new PostRequest(Resource.Create("http://example.com/companies"));

            It should_have_null_body = () =>
                request.Body.ShouldBeOfExactType<PostRequestBody>();
        }

        class when_building_request_url_with_object_body
        {
            Establish context = () =>
            {
                var resource = Resource.Create("http://example.com/companies");
                resource.AddParameter("makemoney", "allday");

                request = new PostRequest(resource, new ObjectRequestBody(new { }));
            };

            It should_generate_query_params = () =>
                request.BuildRequestUrl(new DefaultQuerySerializer()).ShouldEqual("http://example.com/companies?makemoney=allday");
        }

        class when_building_request_url_with_post_request_body
        {
            Establish context = () =>
            {
                var resource = Resource.Create("http://example.com/companies");
                resource.AddParameter("makemoney", "allday");

                request = new PostRequest(resource, new PostRequestBody(Resource.Create("foo")));
            };

            It should_not_generate_query_params = () =>
                request.BuildRequestUrl(new DefaultQuerySerializer()).ShouldEqual("http://example.com/companies");
        }

        class when_building_request_url_with_post_request_body_of_a_file
        {
            static IRequestBody body;

            static IContent serializable;

            static MultipartFormDataContent content;

            static FileUploadByteArray file;

            Establish context = () =>
            {
                var xml = @"<?xml version='1.0'?>
                <someXml xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                  <aDate>2019-02-07</aDate>
                  <aString>Chickens</aString>
                  <aBoolean>false</aBoolean>
                </someXml>";
                var name = "myFile";
                var resource = Resource.Create("http://example.com/companies");

                file = new FileUploadByteArray(name, $"{name}.xml", Encoding.Default.GetBytes(xml));
                resource.AddParameter("makemoney", "allday");
                resource.AddParameter("file", file);

                request = new PostRequest(resource, new PostRequestBody(resource));
                body = request.Body;
            };

            Because of = () =>
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "");
                serializable = body.Serialize(An<ITransmissionSettings>(), An<IQuerySerializer>());
                serializable.WriteTo(httpRequest).Wait();
                content = (MultipartFormDataContent) httpRequest.Content;
            };

            It should_contain_two_parameters = () =>
                request.Resource.Parameters.Count().ShouldEqual(2);

            It should_contain_the_file_data = () =>
            {
                var result = content.ReadAsStringAsync();
                result.Wait();
                result.Result.ShouldContain(@"<aString>Chickens</aString>");
            };
        }
    }
}
