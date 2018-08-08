using System.IO;
using System.Net;
using System.Text;
using SpeakEasy.IntegrationTests.Controllers;
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
        public async void ShouldDownloadFileAsByteArray()
        {
            var contents = await client.Get("invoices/:id", new { id = 5 })
                .OnOk()
                .AsByteArray();

            var contentsAsString = Encoding.UTF8.GetString(contents);

            Assert.Equal("file contents", contentsAsString);
        }

        [Fact]
        public async void ShouldDownloadFileAsFile()
        {
            var file = await client.Get("invoices/:id", new { id = 5 })
                .OnOk()
                .AsFile();

            Assert.Equal("application/octet-stream", file.ContentType);
            Assert.Equal("foo.txt", file.FileName);
            //Assert.Equal("name", file.Name);

            var stream = new MemoryStream();
            await file.WriteToAsync(stream);

            stream.Position = 0;
            string contentsAsString;
            using (var reader = new StreamReader(stream))
            {
                contentsAsString = await reader.ReadToEndAsync();
            }

            Assert.Equal("file contents", contentsAsString);
        }

        [Fact]
        public async void ShouldUploadOneFileByteArray()
        {
            var file = FileUpload.FromBytes("name", "file.txt", Encoding.ASCII.GetBytes("File Content"));
            string iAmNull = null;

            var result = await client
                .Post(file, "invoices", new { param1 = "bob", param2 = "fribble", iAmNull })
                .On(HttpStatusCode.OK)
                .As<FileUploadResult>();

            Assert.Equal(3, result.Parameters.Length);
            Assert.Contains(result.Parameters, x => x.Key == "param1" && x.Value == "bob");
            Assert.Contains(result.Parameters, x => x.Key == "param2" && x.Value == "fribble");
            Assert.Contains(result.Parameters, x => x.Key == "iAmNull" && string.IsNullOrWhiteSpace(x.Value));

            Assert.Single(result.TextFileInfos);
            Assert.Contains(result.TextFileInfos, x => x.FileName == "file.txt" && x.Content == "File Content");
        }

        [Fact]
        public async void ShouldUploadManyFileByteArray()
        {
            var files = new IFile[]
            {
                FileUpload.FromBytes("first file", "first.txt", Encoding.ASCII.GetBytes("First Content")),
                FileUpload.FromBytes("second file", "second.txt", Encoding.ASCII.GetBytes("Second Content")),
                FileUpload.FromBytes("third file", "third.txt", Encoding.ASCII.GetBytes("Third Content"))
            };

            var result = await client
                .Post(files, "invoices", new { id = 123 })
                .On(HttpStatusCode.OK)
                .As<FileUploadResult>();

            Assert.Single(result.Parameters);
            Assert.Contains(result.Parameters, x => x.Key == "id" && x.Value == "123");

            Assert.Equal(3, result.TextFileInfos.Length);
            Assert.Contains(result.TextFileInfos, x => x.FileName == "first.txt" && x.Content == "First Content");
            Assert.Contains(result.TextFileInfos, x => x.FileName == "second.txt" && x.Content == "Second Content");
            Assert.Contains(result.TextFileInfos, x => x.FileName == "third.txt" && x.Content == "Third Content");
        }
    }
}
