using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
// using SpeakEasy.Reflection;
using Newtonsoft.Json;

namespace SpeakEasy.Serializers
{
    public class DefaultJsonSerializer : Serializer
    {
        public override IEnumerable<string> SupportedMediaTypes => new[]
        {
            "application/json",
            "text/json",
            "text/x-json",
            "text/javascript"
        };

        public override Task SerializeAsync<T>(Stream stream, T body)
        {
            JsonSerializer ser = new JsonSerializer();
            using (var sw = new StreamWriter(stream))
            using (var jsonTextWriter = new JsonTextWriter(sw))
            {
                ser.Serialize(jsonTextWriter, body);
            }

            return Task.FromResult(true);
        }

        public override T Deserialize<T>(Stream body, DeserializationSettings deserializationSettings)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(body))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }

        public override object Deserialize(Stream body, DeserializationSettings deserializationSettings, Type type)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(body))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize(jsonTextReader);
            }
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
    }
}
