using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Resticle.Serializers
{
    public class JsonDotNetSerializer : ISerializer
    {
        private Lazy<JsonSerializer> serializer;

        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

        public JsonDotNetSerializer()
        {
            serializer = new Lazy<JsonSerializer>(CreateSerializer);
        }

        public string MediaType
        {
            get { return SupportedMediaTypes.First(); }
        }

        public IEnumerable<string> SupportedMediaTypes
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

        public string Serialize<T>(T t)
        {
            return JsonConvert.SerializeObject(t, jsonSerializerSettings);
        }

        public T Deserialize<T>(string body)
        {
            return JsonConvert.DeserializeObject<T>(body);
        }

        public T Deserialize<T>(string body, DeserializationSettings deserializationSettings)
        {
            JToken parsed = JObject.Parse(body);

            if (deserializationSettings.HasRootElementPath)
            {
                parsed = parsed[deserializationSettings.RootElementPath];
            }

            return parsed.ToObject<T>(serializer.Value);
        }
    }
}
