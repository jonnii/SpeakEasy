using System;
using System.Collections.Generic;
using System.Linq;
using SpeakEasy.Authenticators;
using SpeakEasy.Middleware;
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
            NamingConvention = new DefaultNamingConvention();
            ArrayFormatter = new MultipleValuesArrayFormatter();

            Serializers.Add(new DefaultJsonSerializer());
            Serializers.Add(new TextPlainSerializer());

            Middleware.Append(new TimeoutMiddleware());
            Middleware.Append(new UserAgentMiddleware());
        }

        /// <summary>
        /// Any custom authentication required to access the http api
        /// </summary>
        public IAuthenticator Authenticator { get; set; }

        /// <summary>
        /// The available serialiazers
        /// </summary>
        public List<ISerializer> Serializers { get; set; }

        /// <summary>
        /// The available middleware
        /// </summary>
        public MiddlewareCollection Middleware { get; } = new MiddlewareCollection();

        /// <summary>
        /// The array formatter that will be used to format query string array paramters
        /// </summary>
        public IQuerySerializer ArrayFormatter { get; set; }

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
        public bool IsValid => Serializers.Any() && ArrayFormatter != null;

        /// <summary>
        /// The default timeout for the HttpClient to 30 minutes, 
        /// to use the system default (100 seconds) set this property to null.
        /// </summary>
        public TimeSpan? DefaultTimeout { get; set; } = TimeSpan.FromMinutes(30);

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
                throw new InvalidOperationException("There are no configured serializers, you may have forgotten to add a serializer to the settings.");
            }

            throw new InvalidOperationException("The http client settings are not valid.");
        }
    }
}
