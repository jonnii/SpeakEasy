using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpeakEasy
{
    public class Resource
    {
        private static ConcurrentDictionary<string, string[]> segmentsCache = new ConcurrentDictionary<string, string[]>();

        public static Resource Create(string path)
        {
            var segments = segmentsCache.GetOrAdd(path, GetSegments);
            return new Resource(path.TrimEnd('/'), segments);
        }

        private static string[] GetSegments(string path)
        {
            var segmentMatches = Regex.Matches(path, ":([a-zA-Z-]+)");

            return segmentMatches
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .ToArray();
        }

        private readonly string[] segmentNames;

        private List<Parameter> parameters;

        private Resource(string path, string[] segmentNames)
        {
            Path = path;
            this.segmentNames = segmentNames;
        }

        public string Path { get; }

        public string[] SegmentNames => segmentNames;

        public int NumSegments => segmentNames.Length;

        public bool HasSegments => segmentNames.Any();

        public IEnumerable<Parameter> Parameters => parameters ?? Enumerable.Empty<Parameter>();

        public bool HasParameters => parameters != null && parameters.Any();

        public int NumParameters => parameters == null ? 0 : parameters.Count;

        public bool HasSegment(string name)
        {
            return segmentNames.Contains(name);
        }

        public void AddParameter(string name, object value)
        {
            (parameters ?? (parameters = new List<Parameter>())).Add(new Parameter(name, value));
        }

        public bool HasParameter(string name)
        {
            return parameters != null && parameters.Any(p => p.Name == name);
        }

        public Resource Append(Resource resource)
        {
            var combined =
                resource.Path.StartsWith("/")
                    ? string.Concat(Path, resource.Path)
                    : string.Concat(Path, "/", resource.Path);

            return Create(combined);
        }

        public Resource Append(string resource)
        {
            return Append(Create(resource));
        }

        public string GetEncodedParameters(IArrayFormatter arrayFormatter)
        {
            var formattedParameters = GetFormattedParameters(arrayFormatter);

            return string.Join("&", formattedParameters);
        }

        private IEnumerable<string> GetFormattedParameters(IArrayFormatter arrayFormatter)
        {
            if (parameters == null)
            {
                return Enumerable.Empty<string>();
            }

            return parameters
                .Where(p => p.HasValue)
                .Select(p => p.ToQueryString(arrayFormatter));
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
