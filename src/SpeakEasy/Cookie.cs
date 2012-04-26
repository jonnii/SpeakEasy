using System;

namespace SpeakEasy
{
    public class Cookie
    {
        public Cookie(
            string comment,
            Uri commentUri,
            bool discard,
            string domain,
            bool expired,
            DateTime expires,
            bool httponly,
            string name,
            string path,
            string port,
            bool secure,
            DateTime timeStamp,
            string value,
            int version)
        {
            Comment = comment;
            CommentUri = commentUri;
            Discard = discard;
            Domain = domain;
            Expired = expired;
            Expires = expires;
            Httponly = httponly;
            Name = name;
            Path = path;
            Port = port;
            Secure = secure;
            TimeStamp = timeStamp;
            Value = value;
            Version = version;
        }

        public string Comment { get; private set; }

        public Uri CommentUri { get; private set; }

        public bool Discard { get; private set; }

        public string Domain { get; private set; }

        public bool Expired { get; private set; }

        public DateTime Expires { get; private set; }

        public bool Httponly { get; private set; }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public string Port { get; private set; }

        public bool Secure { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public string Value { get; set; }

        public int Version { get; set; }
    }
}