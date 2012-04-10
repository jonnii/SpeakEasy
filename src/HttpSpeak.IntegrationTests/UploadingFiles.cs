using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;

namespace HttpSpeak.IntegrationTests
{
    [TestFixture]
    public class UploadingFiles : WithApi
    {
        [Test]
        public void ShouldUploadOneFile()
        {
            var file = new FileUpload("name", "filename", new byte[] { 0xDE });

            var fileNames = client.Post(file, "invoices/:id", new { id = 1234 })
                .On(HttpStatusCode.Created).Unwrap<IEnumerable<string>>();

            Assert.That(fileNames.Single(), Is.EqualTo("name"));
        }

        //[Test, Explicit("WIP")]
        //public void ShouldUploadMultipleFiles()
        //{
        //    var files = new[] { new FileUpload("name"), new FileUpload("name") };

        //    var fileNames = client.Post(files, "invoices/:id", new { id = 1234 })
        //        .On(HttpStatusCode.Created).Unwrap<IEnumerable<string>>();

        //    Assert.That(fileNames.Single(), Is.EqualTo("name"));
        //}
    }
}