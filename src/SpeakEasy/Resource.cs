using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SpeakEasy
{
    public class Resource
    {
        private readonly string[] segmentNames;

        private readonly List<Parameter> parameters = new List<Parameter>();

        public Resource(string path)
        {
            Path = path.TrimEnd('/');

            segmentNames = ExtractSegments();
        }

        public string Path { get; private set; }

        public string[] SegmentNames
        {
            get { return segmentNames; }
        }

        public int NumSegments
        {
            get { return segmentNames.Length; }
        }

        public bool HasSegments
        {
            get { return segmentNames.Any(); }
        }

        public IEnumerable<Parameter> Parameters
        {
            get { return parameters; }
        }

        public bool HasParameters
        {
            get { return parameters.Any(); }
        }

        public int NumParameters
        {
            get { return parameters.Count; }
        }

        public bool HasSegment(string name)
        {
            return segmentNames.Contains(name);
        }

        private string[] ExtractSegments()
        {
            var segmentMatches = Regex.Matches(Path, ":([a-zA-Z-]+)");

            return segmentMatches
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .ToArray();
        }

        public void AddParameter(string name, object value)
        {
            parameters.Add(new Parameter(name, value));
        }

        public bool HasParameter(string name)
        {
            return parameters.Any(p => p.Name == name);
        }

        public Resource Append(Resource resource)
        {
            var combined =
                resource.Path.StartsWith("/")
                    ? string.Concat(Path, resource.Path)
                    : string.Concat(Path, "/", resource.Path);

            return new Resource(combined);
        }

        public Resource Append(string resource)
        {
            return Append(new Resource(resource));
        }

        public string GetEncodedParameters()
        {
            var encodableParameters = Parameters.Where(p => p.HasValue).Select(p => p.ToQueryString());
            return string.Join("&", encodableParameters);
        }

        public override string ToString()
        {
            var formattedParameters = parameters.Where(p => p.HasValue).Select(s => s.ToQueryString()).ToList();
            var parameterList = formattedParameters.Any() ? string.Join(", ", formattedParameters) : "none";

            return string.Format("[Resource Path={0}, Parameters={1}]", Path, parameterList);
        }
    }
}
