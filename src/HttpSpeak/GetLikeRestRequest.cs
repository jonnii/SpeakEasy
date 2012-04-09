namespace HttpSpeak
{
    public abstract class GetLikeRestRequest : RestRequest
    {
        protected GetLikeRestRequest(Resource resource)
            : base(resource) { }

        protected override string BuildRequestUrl(Resource resource)
        {
            if (!resource.HasParameters)
            {
                return resource.Path;
            }

            var queryString = resource.GetEncodedParameters();

            return string.Concat(resource.Path, "?", queryString);
        }

        protected override string CalculateContentType(ITransmissionSettings transmissionSettings)
        {
            return transmissionSettings.DefaultSerializerContentType;
        }
    }
}