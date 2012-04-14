using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace SpeakEasy.IntegrationTests
{
    [TestFixture]
    public class UploadingDownloadingFiles : WithApi
    {
        [Test]
        public void ShouldDownloadFile()
        {
            var contents = client.Get("invoices/:id", new { id = 5 })
                .OnOk()
                .AsByteArray();

            var contentsAsString = Encoding.Default.GetString(contents);

            Assert.That(contentsAsString, Is.EqualTo("file contents"));
        }

        [Test]
        public void ShouldUploadOneFile()
        {
            var file = new FileUpload("name", "filename", new byte[] { 0xDE });

            var fileNames = client.Post(file, "invoices")
                .On(HttpStatusCode.Created).As<string[]>();

            Assert.That(fileNames.Single(), Is.EqualTo("name"));
        }

        [Test, Explicit("WIP")]
        public void ShouldUploadMultipleFiles()
        {
            var files = new[] { new FileUpload("name", "filename", new byte[] { 0xDE }), new FileUpload("name", "filename", new byte[] { 0xDE }) };

            var fileNames = client.Post(files, "invoices/:id", new { id = 1234 })
                .On(HttpStatusCode.Created).As<IEnumerable<string>>();

            Assert.That(fileNames.Single(), Is.EqualTo("name"));
        }
    }
}