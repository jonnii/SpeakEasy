namespace SpeakEasy
{
    /// <summary>
    /// The resource merger is responsible for merging a resource with segments.
    /// </summary>
    public interface IResourceMerger
    {
        /// <summary>
        /// Merges a resource with segments
        /// </summary>
        /// <param name="resource">The resource to merge</param>
        /// <param name="segments">An anonymous object representing the segments to merge</param>
        /// <param name="shouldMergeProperties">Whether or not the resource merge properties</param>
        /// <returns>A merged resource</returns>
        Resource Merge(Resource resource, object segments, bool shouldMergeProperties = true);
    }
}