using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HttpSpeak.Serializers
{
    public class DotNetXmlSerializer : ISerializer
    {
        public string MediaType
        {
            get { return SupportedMediaTypes.First(); }
        }

        public IEnumerable<string> SupportedMediaTypes
        {
            get { return new[] { "text/xml" }; }
        }

        public string Serialize<T>(T t)
        {
            using (var writer = new StringWriterWithEncoding(Encoding.UTF8))
            {
                var serializer = new XmlSerializer(t.GetType());
                serializer.Serialize(writer, t);
                return writer.ToString();
            }
        }

        public T Deserialize<T>(string body)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(body))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public T Deserialize<T>(string body, DeserializationSettings deserializationSettings)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// This exists because it's not possible to set the encoding on a string writer
        /// <see cref="http://stackoverflow.com/a/1564727/4590"/>
        /// </summary>
        private class StringWriterWithEncoding : StringWriter
        {
            private readonly Encoding encoding;

            public StringWriterWithEncoding(Encoding encoding)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return encoding; }
            }
        }
    }
}