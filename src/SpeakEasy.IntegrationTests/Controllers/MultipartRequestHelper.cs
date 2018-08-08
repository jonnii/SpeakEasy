using System;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace SpeakEasy.IntegrationTests.Controllers
{
    public static class MultipartRequestHelper
    {
        // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
        // The spec says 70 characters is a reasonable limit.
        public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
            if (!boundary.HasValue)
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }

            if (boundary.Length > lengthLimit)
            {
                throw new InvalidDataException(
                    $"Multipart boundary length limit {lengthLimit} exceeded.");
            }

            return boundary.Value;
        }

        public static bool IsMultipartContentType(string contentType)
        {
            return contentType?.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool HasAttachment(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            return contentDisposition?.DispositionType.Value == "attachment"
                && (contentDisposition.FileName.HasValue || contentDisposition.FileNameStar.HasValue);
        }

        public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="key";
            return contentDisposition?.DispositionType.Value == "form-data"
                && (!contentDisposition.FileName.HasValue || !contentDisposition.FileNameStar.HasValue);
        }
    }
}
