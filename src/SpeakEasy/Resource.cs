using System.Collections.Generic;
using System.Linq;
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

        public string Path { get; }

        public string[] SegmentNames => segmentNames;

        public int NumSegments => segmentNames.Length;

        public bool HasSegments => segmentNames.Any();

        public IEnumerable<Parameter> Parameters => parameters;

        public bool HasParameters => parameters.Any();

        public int NumParameters => parameters.Count;

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

        public string GetEncodedParameters(IArrayFormatter arrayFormatter)
        {
            var formattedParameters = GetFormattedParameters(arrayFormatter);

            return string.Join("&", formattedParameters);
        }

        private IEnumerable<string> GetFormattedParameters(IArrayFormatter arrayFormatter)
        {
            return parameters
                .Where(p => p.HasValue)
                .Select(p => p.ToQueryString(arrayFormatter));
        }
    }
}
