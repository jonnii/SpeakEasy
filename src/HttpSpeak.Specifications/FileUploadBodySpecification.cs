using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class FileUploadBodySpecification
    {
        [Subject(typeof(FileUploadBody))]
        public class when_serializing : with_file_upload_body
        {
            Because of = () =>
                serializable = body.Serialize(transmissionSettings);

            It should_have_content_type_for_multipart_form_data = () =>
                serializable.ShouldBeOfType<MultipartMimeDocumentBody>();

            static ISerializableBody serializable;
        }

        public class with_file_upload_body : WithFakes
        {
            Establish context = () =>
            {
                body = new FileUploadBody(new Resource("http://example.com/fribble/frabble"), new[] { An<IFile>() });
                transmissionSettings = An<ITransmissionSettings>();
            };

            protected static FileUploadBody body;

            protected static ITransmissionSettings transmissionSettings;
        }
    }
}
