using Xunit;

namespace Cimpress.TagliatelleNetCore.UnitTests
{
    [CollectionDefinition("WireMock collection")]
    public class WireMockCollection : ICollectionFixture<WireMockFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}