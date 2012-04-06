using Newtonsoft.Json;

namespace Resticle.Deserializers
{
    public class JsonDeserializer : IDeserializer
    {
        public T Deserialize<T>(string body)
        {
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
