using Newtonsoft.Json;

namespace Resticle.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public string ContentType
        {
            get { return "application/json"; }
        }

        public string Serialize<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}
