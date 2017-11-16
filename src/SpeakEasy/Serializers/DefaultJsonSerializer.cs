using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SpeakEasy.Serializers
{
    public class DefaultJsonSerializer : ISerializer
    {
        public IEnumerable<string> SupportedMediaTypes => new[]
        {
            "application/json",
            "text/json",
            "text/x-json",
            "text/javascript"
        };

        public string MediaType => SupportedMediaTypes.First();

        public void Serialize<T>(Stream stream, T body)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(stream, new System.Text.UTF8Encoding(false), 1024, true))
            {
                using (var jsonTextWriter = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jsonTextWriter, body);
                }
            }
        }

        public T Deserialize<T>(Stream body)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(body))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }

        public object Deserialize(Stream body, Type type)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(body))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize(jsonTextReader, type);
            }
        }
    }
}
