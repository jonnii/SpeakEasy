using System;
using System.Collections.Generic;
using System.Linq;
using HttpSpeak.Authenticators;
using HttpSpeak.Serializers;

namespace HttpSpeak
{
    public class HttpClientSettings
    {
        public static HttpClientSettings Default
        {
            get
            {
                var settings = new HttpClientSettings();

                settings.Serializers.Add(new JsonDotNetSerializer());
                settings.Serializers.Add(new DotNetXmlSerializer());

                return settings;
            }
        }

        public HttpClientSettings()
        {
            Serializers = new List<ISerializer>();
            Authenticator = new NullAuthenticator();

            UserAgent = "HttpSpeak";
        }

        public IAuthenticator Authenticator { get; set; }

        public List<ISerializer> Serializers { get; set; }

        public ISerializer DefaultSerializer
        {
            get { return Serializers.First(); }
        }

        public string UserAgent { get; set; }

        public void Configure<T>(Action<T> configurationCallback)
            where T : ISerializer
        {
            var serializers = Serializers.OfType<T>();
            foreach (var serializer in serializers)
            {
                configurationCallback(serializer);
            }
        }
    }
}