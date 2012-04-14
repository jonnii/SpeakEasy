using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SpeakEasy.Serializers
{
    public class DotNetXmlSerializer : StringBasedSerializer
    {
        public override IEnumerable<string> SupportedMediaTypes
        {
            get { return new[] { "text/xml" }; }
        }

        public override string Serialize<T>(T t)
        {
            using (var writer = new StringWriterWithEncoding(Encoding.UTF8))
            {
                var serializer = new XmlSerializer(t.GetType());
                serializer.Serialize(writer, t);
                return writer.ToString();
            }
        }

        public override T DeserializeString<T>(string body, DeserializationSettings deserializationSettings)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(body))
            {
                return (T)serializer.Deserialize(reader);
            }
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