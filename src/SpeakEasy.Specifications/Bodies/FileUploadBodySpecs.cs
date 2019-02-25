using System.Net.Http;
using System.Text;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Bodies;
using SpeakEasy.Contents;

namespace SpeakEasy.Specifications.Bodies
{
    [Subject(typeof(FileUploadBody))]
    class FileUploadBodySpecs : WithFakes
    {
        static FileUploadBody body;

        static ITransmissionSettings transmissionSettings;

        static Resource resource;

        private Establish context = () =>
        {
            resource = Resource.Create("http://example.com/fribble/frabble");
            body = new FileUploadBody(resource, new[] { An<IFile>() });
            transmissionSettings = An<ITransmissionSettings>();
        };

        class in_general
        {
            It should_consume_resource = () =>
                body.ConsumesResourceParameters.ShouldBeTrue();
        }

        class when_serializing
        {
            static IContent serializable;

            Because of = () =>
                serializable = body.Serialize(transmissionSettings, An<IArrayFormatter>());

            It should_have_content_type_for_multipart_form_data = () =>
                serializable.ShouldBeOfExactType<MultipartMimeContent>();
        }
        class when_serializing_
        {
            static IContent serializable;

            Because of = () =>
                serializable = body.Serialize(transmissionSettings, An<IArrayFormatter>());

            It should_have_content_type_for_multipart_form_data = () =>
                serializable.ShouldBeOfExactType<MultipartMimeContent>();
        }

        class when_serializing_a_multipart_form
        {
            static IContent serializable;

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

                file = new FileUploadByteArray(name, $"{name}.xml", Encoding.Default.GetBytes(xml));
                resource = Resource.Create("http://example.com/fribble/frabble");
                body = new FileUploadBody(resource, new[] { An<IFile>() });
                transmissionSettings = An<ITransmissionSettings>();
            };

            Because of = () =>
            {
                resource = Resource.Create("http://example.com/fribble/frabble");
                resource.AddParameter("file", file);
                body = new FileUploadBody(resource, new[] { An<IFile>() });

                transmissionSettings = An<ITransmissionSettings>();
                serializable = body.Serialize(transmissionSettings, An<IArrayFormatter>());
            };

            It should_have_content_type_for_multipart_form_data = () =>
                serializable.ShouldBeOfExactType<MultipartMimeContent>();
        }
    }
}
