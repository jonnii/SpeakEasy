using System;
using System.IO;

namespace SpeakEasy.Serializers
{
    public abstract class StringBasedSerializer : Serializer
    {
        public override T Deserialize<T>(Stream body)
        {
            return ReadStream(
                body,
                contents => DeserializeString<T>(contents));
        }

        public override object Deserialize(Stream body, Type type)
        {
            return ReadStream(
                body,
                contents => DeserializeString(contents, type));
        }

        private T ReadStream<T>(Stream body, Func<string, T> callback)
        {
            using (var reader = new StreamReader(body))
            {
                var contents = reader.ReadToEnd();
                return callback(contents);
            }
        }

        public abstract T DeserializeString<T>(string body);

        public abstract object DeserializeString(string body, Type type);
    }
}