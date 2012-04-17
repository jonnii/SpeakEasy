namespace SpeakEasy
{
    public interface IResourceMerger
    {
        Resource Merge(Resource resource, object segments, bool shouldMergeProperties = true);
    }
}