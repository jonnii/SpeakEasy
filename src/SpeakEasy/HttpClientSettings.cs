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
            CookieStrategy = new TransientCookieStrategy();
            ArrayFormatter = new MultipleValuesArrayFormatter();

            Serializers.Add(new DefaultJsonSerializer());
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
        /// The cooking container will be reused on all subsequent requests
        /// </summary>
        public ICookieStrategy CookieStrategy { get; set; }

        /// <summary>
        /// The array formatter that will be used to format query string array paramters
        /// </summary>
        public IArrayFormatter ArrayFormatter { get; set; }

        /// <summary>
        /// The default serializer
        /// </summary>
        public ISerializer DefaultSerializer => Serializers.First();

        /// <summary>
        /// Indicates whether or not these settings have a custom authenticator set
        /// </summary>
        public bool HasAuthenticator => Authenticator != null;

        /// <summary>
        /// The naming convention to use when converting segments to query string parameters
        /// </summary>
        public INamingConvention NamingConvention { get; set; }

        /// <summary>
        /// Indicates whether or not the http client settings are valid
        /// </summary>
        public bool IsValid => Serializers.Any() && CookieStrategy != null && ArrayFormatter != null;

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
            if (IsValid)
            {
                return;
            }

            if (!Serializers.Any())
            {
                throw new ConfigurationException("There are no configured serializers, you may have forgotten to add a serializer to the settings.");
            }

            if (CookieStrategy == null)
            {
                throw new ConfigurationException("A cookie strategy is required.");
            }

            throw new ConfigurationException("The http client settings are not valid.");
        }
    }
}
