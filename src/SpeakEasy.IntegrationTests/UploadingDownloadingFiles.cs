using System.IO;
using System.Text;
using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [Collection("Api collection")]
    public class UploadingDownloadingFiles
    {
        private readonly IHttpClient client;

        public UploadingDownloadingFiles(ApiFixture fixture)
        {
            client = fixture.Client;
        }

        [Fact]
        public void ShouldDownloadFileAsByteArray()
        {
            var contents = client.Get("invoices/:id", new { id = 5 })
                .OnOk()
                .AsByteArray();

            var contentsAsString = Encoding.UTF8.GetString(contents);

            Assert.Equal("file contents", contentsAsString);
        }

        [Fact]
        public void ShouldDownloadFileAsFile()
        {
            var file = client.Get("invoices/:id", new { id = 5 })
                .OnOk()
                .AsFile();

            Assert.Equal("application/octet-stream", file.ContentType);
            Assert.Equal("foo.txt", file.FileName);
            //Assert.Equal("name", file.Name);

            var stream = new MemoryStream();
            file.WriteToAsync(stream).Wait();

            stream.Position = 0;
            string contentsAsString;
            using (var reader = new StreamReader(stream))
            {
                contentsAsString = reader.ReadToEnd();
            }

            Assert.Equal("file contents", contentsAsString);
        }

        //[Test]
        //public void ShouldUploadOneFileByteArray()
        //{
        //    var file = FileUpload.FromBytes("name", "filename", new byte[] { 0xDE });

        //    var fileNames = client.Post(file, "invoices")
        //        .On(HttpStatusCode.Created).As<string[]>();

        //    Assert.That(fileNames.Single(), Is.EqualTo("\"name\""));
        //}

        //[Test]
        //public void ShouldUploadMultipleFilesByteArray()
        //{
        //    var files = new[] { FileUpload.FromBytes("name1", "filename", new byte[] { 0xDE }), FileUpload.FromBytes("name2", "filename", new byte[] { 0xDE }) };

        //    var fileNames = client.Post(files, "invoices/:id", new { id = 1234 })
        //        .On(HttpStatusCode.Created).As<IEnumerable<string>>();

        //    Assert.That(fileNames.First(), Is.EqualTo("\"name1\""));
        //    Assert.That(fileNames.Last(), Is.EqualTo("\"name2\""));
        //}
    }
}
