using System.Collections.Generic;
using Newtonsoft.Json;

namespace Resticle.Deserializers
{
    public class JsonDeserializer : IDeserializer
    {
        public IEnumerable<string> SupportedMediaTypes
        {
            get { return new[] { "application/json" }; }
        }

        public T Deserialize<T>(string body)
        {
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
