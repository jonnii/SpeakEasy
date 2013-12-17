namespace SpeakEasy
{
    public abstract class GetLikeRequest : HttpRequest
    {
        protected GetLikeRequest(Resource resource)
            : base(resource, new NullRequestBody())
        {

        }

        public override string BuildRequestUrl(IArrayFormatter arrayFormatter)
        {
            if (!Resource.HasParameters)
            {
                return Resource.Path;
            }

            var queryString = Resource.GetEncodedParameters(arrayFormatter);

            return string.Concat(Resource.Path, "?", queryString);
        }
    }
}