using System;
using System.Net;
using System.Threading.Tasks;
using Cimpress.TagliatelleNetCore.Data;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Cimpress.TagliatelleNetCore.UnitTests
{
    [Collection("WireMock collection")]

    public class LowLevelClientTest
    {
        private readonly LowLevelClient _lowLevelClient;
        private readonly WireMockFixture _fixture;

        public LowLevelClientTest(WireMockFixture fixture)
        {
            _fixture = fixture;
            _lowLevelClient = new LowLevelClient("Zndlcmd2MjN0MjN0NTUzNjVmZmZld3FkMndlZmMzNDUy", "http://localhost:8089");
            _fixture.Server.Reset();
        }

        [Fact]
        public void PostTagHandlesUnauthorizedGracefully()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v0/tags").UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(401)
                        .WithBody("{}")
                );

            var tag = new TagRequest();
            var response = _lowLevelClient.postTag(tag);

            Assert.Equal(response.Result.StatusCode, HttpStatusCode.Unauthorized);
        }
        
        [Fact]
        public void PostReturnsTheBodyOfCreatedTag()
        {
            _fixture.Server
                .Given(Request.Create().WithPath("/v0/tags").UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBodyAsJson(new {
                            resourceUri = "http://some.resource",
                            key = "urn:my-service:tag",
                            value = "bla bla bla",
                            createdAt = "2018-11-01T09:27:17+00:00",
                            createdBy= "auth0|a767ex734cv376vx346xv34",
                            modifiedAt = "2018-11-01T09:27:18+00:00",
                            modifiedBy = "auth0|sdfv454353543534534534"
                        })    
                );

            var tag = new TagRequest{Key = "urn:tagspace:tag", ResourceUri = "http://some.resource.url", Value = "Some value"};
            var response = _lowLevelClient.postTag(tag);
            var result = response.Result.Data;
            Assert.Equal("http://some.resource", result.ResourceUri);
            Assert.Equal("urn:my-service:tag", result.Key);
            Assert.Equal("bla bla bla", result.Value);
            Assert.Equal("2018-11-01T09:27:17+00:00", result.CreatedAt);
            Assert.Equal("auth0|a767ex734cv376vx346xv34",result.CreatedBy);
            Assert.Equal("2018-11-01T09:27:18+00:00", result.ModifiedAt);
            Assert.Equal("auth0|sdfv454353543534534534", result.ModifiedBy);
        }
        
        [Fact]
        public void PutTagHandlesUnauthorizedGracefully() {
            _fixture.Server
                .Given(Request.Create().WithPath("/v0/tags/0").UsingPut())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(401)
                        .WithBody("{}")
                );

            var tag = new TagRequest() { Key = "urn:tagspace:tag", ResourceUri = "http://some.resource.url", Value = "Some value" };
            var response = _lowLevelClient.putTag("0", tag);

            Assert.Equal(response.Result.StatusCode, HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void DeleteTagHandlesUnauthorizedGracefully() {
            _fixture.Server
                .Given(Request.Create().WithPath("/v0/tags/0").UsingDelete())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(401)
                        .WithBody("{}")
                );

            var response = _lowLevelClient.deleteTag("0");

            Assert.Equal(response.Result.StatusCode, HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void GetTagsHandlesUnauthorizedGracefully() {
            _fixture.Server
                .Given(Request.Create().WithPath("/v0/tags").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(401)
                        .WithBody("{}")
                );

            var tag = new TagRequest() { Key = "urn:tagspace:tag", ResourceUri = "http://some.resource.url", Value = "Some value" };
            var response = _lowLevelClient.getTags("urn:test", "https://resource.url");

            Assert.Equal(response.Result.StatusCode, HttpStatusCode.Unauthorized);
        }
    }
}
