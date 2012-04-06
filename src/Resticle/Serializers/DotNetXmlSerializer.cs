using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Resticle.Serializers
{
    public class DotNetXmlSerializer : ISerializer
    {
        public string ContentType
        {
            get { return "text/xml"; }
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