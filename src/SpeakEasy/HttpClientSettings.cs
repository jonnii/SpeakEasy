using System;
using System.Collections.Generic;
using System.Linq;
using SpeakEasy.Authenticators;
using SpeakEasy.Serializers;

namespace SpeakEasy
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

            UserAgent = "SpeakEasy";
        }

        public IAuthenticator Authenticator { get; set; }

        public List<ISerializer> Serializers { get; set; }

        public ISerializer DefaultSerializer
        {
            get { return Serializers.First(); }
        }

        public string UserAgent { get; set; }

        public bool HasAuthenticator
        {
            get { return Authenticator != null; }
        }

        public void Configure<T>(Action<T> configurationCallback)
            where T : ISerializer
        {
            var serializers = Serializers.OfType<T>();
            foreach (var serializer in serializers)
            {
                configurationCallback(serializer);
            }
        }

        public void AddAfterRequestCallback(Action<IHttpResponse> callback)
        {
            throw new NotImplementedException();
        }
    }
}