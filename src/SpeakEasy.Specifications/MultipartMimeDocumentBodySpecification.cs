using Machine.Fakes;
using Machine.Specifications;

namespace SpeakEasy.Specifications
{
    public class MultipartMimeDocumentBodySpecification
    {
        [Subject(typeof(MultipartMimeDocumentBody))]
        public class in_general : with_body
        {
            It should_have_content_type = () =>
                body.ContentType.ShouldEqual("multipart/form-data; boundary=---------------------------29772313742745");
        }

        [Subject(typeof(MultipartMimeDocumentBody))]
        public class when_formatting_parameter : with_body
        {
            Because of = () =>
                formatted = body.GetFormattedParameter(new Parameter("email", "foo@bar.com"));

            It should_format_as_mime_field = () =>
                formatted.ShouldEqual("-----------------------------29772313742745\r\nContent-Disposition: form-data; name=\"email\"\r\nfoo@bar.com\r\n");

            static string formatted;
        }

        [Subject(typeof(MultipartMimeDocumentBody))]
        public class when_formatting_footer : with_body
        {
            Because of = () =>
                footer = body.GetFooter();

            It should_format_footer = () =>
                footer.ShouldEqual("-----------------------------29772313742745--\r\n");

            static string footer;
        }

        public class with_body : WithFakes
        {
            Establish context = () =>
                body = new MultipartMimeDocumentBody(new Resource("http://fribble.com"), new[] { An<IFile>() });

            protected static MultipartMimeDocumentBody body;
        }
    }
}
