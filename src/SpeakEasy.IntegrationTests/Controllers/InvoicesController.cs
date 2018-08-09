using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using FormOptions = Microsoft.AspNetCore.Http.Features.FormOptions;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/invoices")]
    public class InvoicesController : Controller
    {
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return File(Encoding.UTF8.GetBytes("file contents"), "application/octet-stream", "foo.txt");
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            var formAccumulator = new KeyValueAccumulator();
            var fileInfos = new List<TextFileInfo>();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                DefaultFormOptions.MultipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync().ConfigureAwait(false);

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition,
                    out ContentDispositionHeaderValue contentDisposition);

                if (!hasContentDispositionHeader)
                {
                    section = await reader.ReadNextSectionAsync().ConfigureAwait(false);
                    continue;
                }

                switch (contentDisposition)
                {
                    case var disposition when MultipartRequestHelper.HasAttachment(disposition):
                        fileInfos.Add(await GetTextFileInfo(section, contentDisposition).ConfigureAwait(false));
                        break;

                    case var disposition when MultipartRequestHelper.HasFormDataContentDisposition(disposition):

                        var (key, value) = await GetKeyAndValue(section, contentDisposition).ConfigureAwait(false);

                        formAccumulator.Append(key, value);

                        if (formAccumulator.ValueCount > DefaultFormOptions.ValueCountLimit)
                        {
                            throw new InvalidDataException($"Form key count limit {DefaultFormOptions.ValueCountLimit} exceeded.");
                        }

                        break;
                }

                section = await reader.ReadNextSectionAsync().ConfigureAwait(false);
            }

            return Ok(new FileUploadResult
            {
                TextFileInfos = fileInfos.ToArray(),
                Parameters = formAccumulator
                    .GetResults()
                    .Select(x => new Parameter { Key = x.Key, Value = x.Value })
                    .ToArray()
            });
        }

        private async Task<(string Key, string Value)> GetKeyAndValue(
            MultipartSection section,
            ContentDispositionHeaderValue contentDisposition)
        {
            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
            var encoding = GetEncoding(section);

            using (var streamReader = new StreamReader(
                section.Body,
                encoding,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: true))
            {
                var value = await streamReader.ReadToEndAsync().ConfigureAwait(false);

                if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                {
                    value = string.Empty;
                }

                return (key.Value, value);
            }
        }

        private async Task<TextFileInfo> GetTextFileInfo(MultipartSection section, ContentDispositionHeaderValue contentDisposition)
        {
            using (var stream = new MemoryStream())
            {
                await section.Body.CopyToAsync(stream).ConfigureAwait(false);

                var text = Encoding.ASCII.GetString(stream.ToArray());

                var fileName = HeaderUtilities
                    .RemoveQuotes(contentDisposition.FileName)
                    .Value;

                return new TextFileInfo { FileName = fileName, Content = text };
            }
        }

        private Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }
    }
}
