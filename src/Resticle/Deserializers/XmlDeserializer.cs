using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

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
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(body))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}