using System;
using System.Net;
using System.Text;

namespace Resticle
{
    public class PutRestRequest : RestRequest
    {
        public PutRestRequest(string url)
            : base(url)
        {
        }

        public Func<string> Body { get; set; }

        public bool HasBody
        {
            get { return Body != null; }
        }

        public override WebRequest BuildWebRequest()
        {
            var baseRequest = base.BuildWebRequest();
            baseRequest.Method = "PUT";
            baseRequest.ContentType = "application/json";

            if (HasBody)
            {
                var bytes = Encoding.Default.GetBytes(Body());

                baseRequest.ContentLength = bytes.Length;
                using (var stream = baseRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            return baseRequest;
        }
    }
}