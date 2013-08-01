using System;
using System.Collections.Generic;
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

        public override IEnumerable<string> SupportedMediaTypes
        {
            get
            {
                return new[]
                {
                    "application/json",
                    "text/json",
                    "text/x-json",
                    "text/javascript"
                };
            }
        }

        public override string Serialize<T>(T t)
        {
            return SimpleJson.SerializeObject(t, JsonSerializerStrategy);
        }

        public override T DeserializeString<T>(string body, DeserializationSettings deserializationSettings)
        {
            if (deserializationSettings.SkipRootElement)
            {
                throw new NotSupportedException("Cannot skip root element with SimpleJsonSerializer");
            }

            if (deserializationSettings.HasRootElementPath)
            {
                throw new NotSupportedException("Cannot navigate root element path with SimpleJsonSerializer");
            }

            return SimpleJson.DeserializeObject<T>(body, JsonSerializerStrategy);
        }

        public class DefaultJsonSerializerStrategy : PocoJsonSerializerStrategy
        {
            protected override object SerializeEnum(Enum p)
            {
                return p.ToString();
            }

            public override object DeserializeObject(object value, Type type)
            {
                var stringValue = value as string;
                if (stringValue != null)
                {
                    if (type.IsEnum)
                    {
                        return Enum.Parse(type, stringValue, true);
                    }

                    if (ReflectionUtils.IsNullableType(type))
                    {
                        var underlyingType = Nullable.GetUnderlyingType(type);
                        if (underlyingType.IsEnum)
                        {
                            return Enum.Parse(underlyingType, stringValue, true);
                        }
                    }
                }

                return base.DeserializeObject(value, type);
            }
        }
    }
}
