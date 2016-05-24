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

        public string Comment { get; }

        public Uri CommentUri { get; }

        public bool Discard { get; }

        public string Domain { get; }

        public bool Expired { get; }

        public DateTime Expires { get; }

        public bool Httponly { get; }

        public string Name { get; }

        public string Path { get; }

        public string Port { get; }

        public bool Secure { get; }

        public DateTime TimeStamp { get; }

        public string Value { get; }

        public int Version { get; }
    }
}
