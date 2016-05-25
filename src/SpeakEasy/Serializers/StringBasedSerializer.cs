using System;
using System.IO;
using System.Text;

namespace SpeakEasy.Serializers
{
    public abstract class StringBasedSerializer : Serializer
    {
        public override T Deserialize<T>(Stream body, DeserializationSettings deserializationSettings)
        {
            return ReadStream(
                body,
                contents => DeserializeString<T>(contents, deserializationSettings));
        }

        public override object Deserialize(Stream body, DeserializationSettings deserializationSettings, Type type)
        {
            return ReadStream(
                body,
                contents => DeserializeString(contents, deserializationSettings, type));
        }

        private T ReadStream<T>(Stream body, Func<string, T> callback)
        {
            using (var reader = new StreamReader(body, Encoding.Default, true, 1024, true))
            {
                var contents = reader.ReadToEnd();
                return callback(contents);
            }
        }

        public T DeserializeString<T>(string body)
        {
            return DeserializeString<T>(body, DefaultDeserializationSettings);
        }

        public abstract T DeserializeString<T>(string body, DeserializationSettings deserializationSettings);

        public abstract object DeserializeString(string body, DeserializationSettings deserializationSettings, Type type);
    }
}
