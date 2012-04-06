using System;
using System.Collections.Generic;

namespace Resticle.Deserializers
{
    public class DotNetXmlDeserializer : IDeserializer
    {
        public IEnumerable<string> SupportedMediaTypes
        {
            get { return new[] { "text/xml" }; }
        }

        public T Deserialize<T>(string body)
        {
            throw new NotSupportedException();
        }
    }
}