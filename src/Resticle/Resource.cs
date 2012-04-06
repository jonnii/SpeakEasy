using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Resticle
{
    public class Resource : DynamicObject
    {
        public static dynamic Create(string path)
        {
            return new Resource(path);
        }

        private readonly string[] segmentNames;

        public Resource(string path)
        {
            Path = path;

            segmentNames = ExtractSegments();
        }

        public string Path { get; private set; }

        public int NumSegments
        {
            get { return segmentNames.Length; }
        }

        private string[] ExtractSegments()
        {
            var segmentMatches = Regex.Matches(Path, ":([a-zA-Z-]+)");

            return segmentMatches
                .Cast<Match>()
                .Select(m => m.Groups[1].Value.ToLower())
                .ToArray();
        }

        public bool HasSegment(string name)
        {
            return segmentNames.Contains(name);
        }

        public bool HasSegments
        {
            get { return segmentNames.Any(); }
        }

        public string Merge(object segments)
        {
            if (!HasSegments)
            {
                return Path;
            }

            var properties = segments
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower());

            var merged = Path;

            foreach (var segmentName in segmentNames)
            {
                PropertyInfo property;
                if (!properties.TryGetValue(segmentName, out property))
                {
                    throw new ArgumentException("Could not find a property matching segment: " + segmentName);
                }

                var propertyValue = property.GetValue(segments, new object[0]);

                merged = merged.Replace(":" + segmentName, propertyValue.ToString());
            }

            return merged;
        }

        public Resource Append(Resource resource)
        {
            var combined =
                Path.EndsWith("/")
                    ? string.Concat(Path, resource.Path)
                    : string.Concat(Path, "/", resource.Path);

            return new Resource(combined);
        }

        public Resource Append(string resource)
        {
            return Append(new Resource(resource));
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methodName = binder.Name;
            var parameter = args[0];

            result = Path.Replace(":" + methodName.ToLower(), parameter.ToString());

            return true;
        }
    }
}
