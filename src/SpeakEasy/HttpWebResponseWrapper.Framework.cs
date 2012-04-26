#if FRAMEWORK

using System;

namespace SpeakEasy
{
    public partial class HttpWebResponseWrapper
    {
        public string Server
        {
            get { return response.Server; }
        }

        public string ContentEncoding
        {
            get { return response.ContentEncoding; }
        }

        public DateTime LastModified
        {
            get { return response.LastModified; }
        }
    }
}

#endif