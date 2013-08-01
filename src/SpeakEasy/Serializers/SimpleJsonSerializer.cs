using System;
using System.Collections.Generic;

namespace SpeakEasy.Serializers
{
    public class SimpleJsonSerializer : StringBasedSerializer
    {
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
            return SimpleJson.SerializeObject(t);
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

            return SimpleJson.DeserializeObject<T>(body);
        }
    }
}
