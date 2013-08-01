using System;
using System.Collections.Generic;
using System.Linq;
using SpeakEasy.Authenticators;
using SpeakEasy.Loggers;
using SpeakEasy.Serializers;

namespace SpeakEasy
{
    /// <summary>
    /// The http client settings class is used to configure the http client
    /// </summary>
    public class HttpClientSettings
    {
        /// <summary>
        /// Creates a new http client settings with blank defaults
        /// </summary>
        public HttpClientSettings()
        {
            Serializers = new List<ISerializer>();
            Authenticator = new NullAuthenticator();
            Logger = new NullLogger();
            NamingConvention = new DefaultNamingConvention();
            UserAgent = SpeakEasy.UserAgent.SpeakEasy;

            Serializers.Add(new DefaultJsonSerializer());
#if FRAMEWORK
            Serializers.Add(new DotNetXmlSerializer());
#endif
        }

        /// <summary>
        /// The logging mechanism the client will use
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Any custom authentication required to access the http api
        /// </summary>
        public IAuthenticator Authenticator { get; set; }

        /// <summary>
        /// The available serialiazers
        /// </summary>
        public List<ISerializer> Serializers { get; set; }

        /// <summary>
        /// The user agent the web client will send when making http requests
        /// </summary>
        public IUserAgent UserAgent { get; set; }

        /// <summary>
        /// The default serializer
        /// </summary>
        public ISerializer DefaultSerializer
        {
            get { return Serializers.First(); }
        }

        /// <summary>
        /// Indicates whether or not this settings has any authenticator set
        /// </summary>
        public bool HasAuthenticator
        {
            get { return Authenticator != null; }
        }

        /// <summary>
        /// The naming convention to use when converting segments to query string parameters
        /// </summary>
        public INamingConvention NamingConvention { get; set; }

        /// <summary>
        /// Indicates whether or not the http client settings are valid
        /// </summary>
        public bool IsValid
        {
            get { return Serializers.Any(); }
        }

        /// <summary>
        /// Configures the give serializer
        /// </summary>
        /// <typeparam name="T">The type of serializer to configure</typeparam>
        /// <param name="configurationCallback">The configuration callback</param>
        public void Configure<T>(Action<T> configurationCallback)
            where T : ISerializer
        {
            var serializers = Serializers.OfType<T>();

            foreach (var serializer in serializers)
            {
                configurationCallback(serializer);
            }
        }

        /// <summary>
        /// Validates the http client settings
        /// </summary>
        public void Validate()
        {
            if (!Serializers.Any())
            {
                throw new HttpException("At least one serializer is required for the http client to function.");
            }
        }
    }
}