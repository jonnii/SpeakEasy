using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpeakEasy
{
    public class ResourceMerger : IResourceMerger
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
                var message = string.Format(
                    "The resource {0} requires the following segments, but none were given: {1}",
                    resource.Path,
                    string.Join(", ", resource.SegmentNames));

                throw new ArgumentException(message);
            }

            var properties = segments
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower());

            var mergedResource = MergeUrlSegments(resource, segments, properties);

            if (!shouldMergeProperties)
            {
                return mergedResource;
            }

            return AddMergedParameters(mergedResource, segments, properties);
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
                PropertyInfo property;
                if (!properties.TryGetValue(segmentName, out property))
                {
                    throw new ArgumentException("Could not find a property matching segment: " + segmentName);
                }

                var propertyValue = property.GetValue(segments, new object[0]);

                if (propertyValue == null)
                {
                    var message = string.Format(
                        "Could not merge url segment with name {0} because the value of the segment was null. " +
                        "When passing in segments for a url make sure each property has a value if it is to be used in the url.",
                        segmentName);

                    throw new ArgumentException(message);
                }

                merged = merged.Replace(":" + segmentName, propertyValue.ToString());

                properties.Remove(segmentName);
            }

            return new Resource(merged);
        }
    }
}