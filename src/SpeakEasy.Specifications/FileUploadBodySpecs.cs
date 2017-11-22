using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Contents;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(FileUploadBody))]
    class FileUploadBodySpecs : WithFakes
    {
        static FileUploadBody body;

        static ITransmissionSettings transmissionSettings;

        Establish context = () =>
        {
            body = new FileUploadBody(new Resource("http://example.com/fribble/frabble"), new[] { An<IFile>() });
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
    }
}
