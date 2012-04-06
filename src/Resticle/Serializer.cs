using System;
using Resticle.Serializers;

namespace Resticle
{
    public class Serializer
    {
        public static readonly Func<ISerializer> Json = () => new JsonSerializer();

        public static readonly Func<ISerializer> Xml = () => new DotNetXmlSerializer();
    }
}