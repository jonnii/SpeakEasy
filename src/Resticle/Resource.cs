using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Resticle
{
    public class Resource
    {
        public static dynamic Create(string path)
        {
            return new Resource(path);
        }

        private readonly string path;

        private readonly string[] segments;

        public Resource(string path)
        {
            this.path = path;

            segments = ExtractSegments();
        }

        public int NumSegments
        {
            get { return segments.Length; }
        }

        private string[] ExtractSegments()
        {
            var segmentMatches = Regex.Matches(path, ":([a-zA-Z-]+)");

            return segmentMatches
                .Cast<Match>()
                .Select(m => m.Groups[1].Value.ToLower())
                .ToArray();
        }

        public bool HasSegment(string name)
        {
            return segments.Contains(name);
        }

        public string Merge(object segmentProvider)
        {
            var properties = segmentProvider
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower());

            var merged = path;

            foreach (var segment in segments)
            {
                PropertyInfo property;
                if (!properties.TryGetValue(segment, out property))
                {
                    throw new ArgumentException("Could not find a property matching segment: " + segment);
                }

                var propertyValue = property.GetValue(segmentProvider, new object[0]);

                merged = merged.Replace(":" + segment, propertyValue.ToString());
            }

            return merged;
        }
    }
}
