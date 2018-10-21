using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpeakEasy
{
    internal class ResourceMerger : IResourceMerger
    {
        public ResourceMerger(INamingConvention namingConvention)
        {
            NamingConvention = namingConvention;
        }

        public INamingConvention NamingConvention { get; set; }

        public Resource Merge(Resource resource, object segments, bool shouldMergeProperties = true)
        {
            if (!resource.HasSegments && segments == null)
            {
                return resource;
            }

            if (segments == null)
            {
                throw new ArgumentException(
                    $"The resource {resource.Path} requires the following segments, but none were given: {string.Join(", ", resource.SegmentNames)}");
            }

            var properties = segments
                .GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .ToDictionary(p => p.Name.ToLower());

            var mergedResource = MergeUrlSegments(resource, segments, properties);

            return shouldMergeProperties
                ? AddMergedParameters(mergedResource, segments, properties)
                : mergedResource;
        }

        private Resource AddMergedParameters(Resource mergedResource, object segments, Dictionary<string, PropertyInfo> properties)
        {
            foreach (var property in properties.Values)
            {
                var parameterName = NamingConvention.ConvertPropertyNameToParameterName(property.Name);
                var parameterValue = property.GetValue(segments, new object[0]);

                mergedResource.AddParameter(parameterName, parameterValue);
            }

            return mergedResource;
        }

        private Resource MergeUrlSegments(Resource resource, object segments, IDictionary<string, PropertyInfo> properties)
        {
            var merged = resource.Path;

            foreach (var segmentName in resource.SegmentNames)
            {
                var lowerSegmentName = segmentName.ToLower();

                if (!properties.TryGetValue(lowerSegmentName, out var property))
                {
                    throw new ArgumentException("Could not find a property matching segment: " + segmentName);
                }

                var propertyValue = property.GetValue(segments, new object[0]);

                if (propertyValue == null)
                {
                    throw new ArgumentException(
                        $"Could not merge url segment with name {segmentName} because the value of the segment was null. " +
                        "When passing in segments for a url make sure each property has a value if it is to be used in the url.");
                }

                merged = merged.Replace(":" + segmentName, propertyValue.ToString());

                properties.Remove(lowerSegmentName);
            }

            return Resource.Create(merged);
        }
    }
}
