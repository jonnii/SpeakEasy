using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpeakEasy.Reflection;

namespace SpeakEasy.Serializers
{
    public class DefaultJsonSerializer : StringBasedSerializer
    {
        public DefaultJsonSerializer()
        {
            JsonSerializerStrategy = new DefaultJsonSerializerStrategy();
        }

        public IJsonSerializerStrategy JsonSerializerStrategy { get; set; }

        public override IEnumerable<string> SupportedMediaTypes => new[]
        {
            "application/json",
            "text/json",
            "text/x-json",
            "text/javascript"
        };

        public override async Task SerializeAsync<T>(Stream stream, T body)
        {
            var content = SimpleJson.SerializeObject(body, JsonSerializerStrategy);

            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(content).ConfigureAwait(false);
            }
        }

        public override T DeserializeString<T>(string body, DeserializationSettings deserializationSettings)
        {
            EnsureCompatibleSettings(deserializationSettings);

            return SimpleJson.DeserializeObject<T>(body, JsonSerializerStrategy);
        }

        public override object DeserializeString(string body, DeserializationSettings deserializationSettings, Type type)
        {
            EnsureCompatibleSettings(deserializationSettings);

            return SimpleJson.DeserializeObject(body, type, JsonSerializerStrategy);
        }

        private void EnsureCompatibleSettings(DeserializationSettings deserializationSettings)
        {
            if (deserializationSettings.SkipRootElement)
            {
                throw new NotSupportedException("Cannot skip root element with SimpleJsonSerializer");
            }

            if (deserializationSettings.HasRootElementPath)
            {
                throw new NotSupportedException("Cannot navigate root element path with SimpleJsonSerializer");
            }
        }

        public class DefaultJsonSerializerStrategy : PocoJsonSerializerStrategy
        {
            protected override object SerializeEnum(Enum value)
            {
                return value.ToString();
            }

            public override object DeserializeObject(object value, Type type)
            {
                var stringValue = value as string;

                if (stringValue == null)
                {
                    return base.DeserializeObject(value, type);
                }

                if (type.IsEnum)
                {
                    return Enum.Parse(type, stringValue, true);
                }

                if (!ReflectionUtils.IsNullableType(type))
                {
                    return base.DeserializeObject(value, type);
                }

                var underlyingType = Nullable.GetUnderlyingType(type);

                return underlyingType.IsEnum
                    ? Enum.Parse(underlyingType, stringValue, true)
                    : base.DeserializeObject(value, type);
            }
        }
    }
}
