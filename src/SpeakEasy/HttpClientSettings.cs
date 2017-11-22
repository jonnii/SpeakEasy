using System;
using System.Collections.Generic;
using System.Linq;
using SpeakEasy.ArrayFormatters;
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
        /// The available serialiazers
        /// </summary>
        internal List<IHttpMiddleware> Middleware { get; } = new List<IHttpMiddleware>
        {
            new UserAgentMiddleware()
        };

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
        public bool IsValid => Serializers.Any() && ArrayFormatter != null;

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

        public int MiddlewareCount => Middleware.Count;

        public void AppendMiddleware(IHttpMiddleware middleware)
        {
            Middleware.Add(middleware);
        }

        public void PrependMiddleware(IHttpMiddleware middleware)
        {
            Middleware.Insert(0, middleware);
        }

        public bool HasMiddleware<TMiddleware>()
            where TMiddleware : IHttpMiddleware
        {
            return Middleware.Any(t => t is TMiddleware);
        }

        public void ReplaceMiddleware<TMiddleware>(TMiddleware replacement)
            where TMiddleware : IHttpMiddleware
        {
            var index = RemoveMiddleware<TMiddleware>();
            Middleware.Insert(index, replacement);
        }

        public int RemoveMiddleware<TMiddleware>()
            where TMiddleware : IHttpMiddleware
        {
            if (!HasMiddleware<TMiddleware>())
            {
                throw new ArgumentException();
            }

            var index = Middleware.FindIndex(t => t is TMiddleware);
            Middleware.RemoveAt(index);
            return index;
        }
    }
}
