using Xunit;

namespace SpeakEasy.IntegrationTests
{
    [CollectionDefinition("Api collection")]
    public class ApiCollection : ICollectionFixture<ApiFixture>
    {
    }
}