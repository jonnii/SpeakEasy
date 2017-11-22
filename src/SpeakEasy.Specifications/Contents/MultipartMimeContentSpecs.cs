using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Contents;

namespace SpeakEasy.Specifications.Contents
{
    public class MultipartMimeContentSpecs
    {
        [Subject(typeof(MultipartMimeContent))]
        public class in_general : with_body
        {
            It should_have_content_type = () =>
                body.ContentType.ShouldEqual("multipart/form-data; boundary=---------------------------29772313742745");
        }

        [Subject(typeof(MultipartMimeContent))]
        public class when_formatting_parameter : with_body
        {
            Because of = () =>
                formatted = body.GetFormattedParameter(new Parameter("email", "foo@bar.com"));

            It should_format_as_mime_field = () =>
                formatted.ShouldEqual("-----------------------------29772313742745\r\nContent-Disposition: form-data; name=\"email\"\r\n\r\nfoo@bar.com\r\n");

            static string formatted;
        }

        [Subject(typeof(MultipartMimeContent))]
        public class when_formatting_file : with_body
        {
            Establish context = () =>
            {
                file = An<IFile>();
                file.WhenToldTo(f => f.Name).Return("file-upload");
                file.WhenToldTo(f => f.FileName).Return("test.txt");
                file.WhenToldTo(f => f.ContentType).Return("text/plain");
            };

            Because of = () =>
                header = body.GetFileHeader(file);

            It should_format_header = () =>
                header.ShouldEqual("-----------------------------29772313742745\r\nContent-Disposition: form-data; name=\"file-upload\"; filename=\"test.txt\"\r\nContent-Type: text/plain\r\n\r\n");

            static IFile file;

            static string header;
        }

        [Subject(typeof(MultipartMimeContent))]
        public class when_formatting_file_without_content_type : with_body
        {
            Establish context = () =>
            {
                file = An<IFile>();
                file.WhenToldTo(f => f.Name).Return("file-upload");
                file.WhenToldTo(f => f.FileName).Return("test.txt");
            };

            Because of = () =>
                header = body.GetFileHeader(file);

            It should_format_header = () =>
                header.ShouldEqual("-----------------------------29772313742745\r\nContent-Disposition: form-data; name=\"file-upload\"; filename=\"test.txt\"\r\nContent-Type: application/octet-stream\r\n\r\n");

            static IFile file;

            static string header;
        }

        [Subject(typeof(MultipartMimeContent))]
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
                body = new MultipartMimeContent(new Resource("http://fribble.com"), new[] { An<IFile>() });

            internal static MultipartMimeContent body;
        }
    }
}
