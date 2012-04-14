using System.IO;

namespace SpeakEasy.Serializers
{
    public abstract class StringBasedSerializer : Serializer
    {
        public override T Deserialize<T>(Stream body, DeserializationSettings deserializationSettings)
        {
            using (var reader = new StreamReader(body))
            {
                var contents = reader.ReadToEnd();

                return DeserializeString<T>(contents, deserializationSettings);
            }
        }

        public T DeserializeString<T>(string body)
        {
            return DeserializeString<T>(body, DefaultDeserializationSettings);
        }

        public abstract T DeserializeString<T>(string body, DeserializationSettings deserializationSettings);
    }
}