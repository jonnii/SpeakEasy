using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpeakEasy.Serializers
{
    public class JsonDotNetSerializer : Serializer
    {
        private Lazy<JsonSerializer> serializer;

        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

        public JsonDotNetSerializer()
        {
            serializer = new Lazy<JsonSerializer>(CreateSerializer);
        }

        public override IEnumerable<string> SupportedMediaTypes
        {
            get { return new[] { "application/json" }; }
        }

        public void ConfigureSettings(Action<JsonSerializerSettings> configureCallback)
        {
            configureCallback(jsonSerializerSettings);
            serializer = new Lazy<JsonSerializer>(CreateSerializer);
        }

        private JsonSerializer CreateSerializer()
        {
            return JsonSerializer.Create(jsonSerializerSettings);
        }

        public override string Serialize<T>(T t)
        {
            return JsonConvert.SerializeObject(t, jsonSerializerSettings);
        }

        public override T Deserialize<T>(string body, DeserializationSettings deserializationSettings)
        {
            var parsed = JToken.Parse(body);

            if (deserializationSettings.HasRootElementPath)
            {
                parsed = parsed[deserializationSettings.RootElementPath];
            }

            if (deserializationSettings.SkipRootElement)
            {
                // this can't be right, but it seems to work.
                parsed = parsed.Children().First().Children().First();
            }

            return parsed.ToObject<T>(serializer.Value);
        }
    }
}
