using System;
using WireMock.Server;

namespace Cimpress.TagliatelleNetCore.UnitTests
{
    public class WireMockFixture : IDisposable

    {
        public FluentMockServer Server { get; }

        public WireMockFixture()
        {
            Server = FluentMockServer.Start(8089);
        }

        public void Dispose()
        {
            Server.Stop();
        }
    }
}