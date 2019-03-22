using System;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Cimpress.TagliatelleNetCore.Data;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Cimpress.TagliatelleNetCore.UnitTests
{
    public class ClientTests : IDisposable
    {
        private readonly Client _client;

        private readonly FluentMockServer _server;

        public ClientTests()
        {
            _server = FluentMockServer.Start(8089);
            _client = new Client("Zndlcmd2MjN0MjN0NTUzNjVmZmZld3FkMndlZmMzNDUy", "http://localhost:8089");
        }

        public void Dispose()
        {
            _server.Stop();   
        }

        [Fact]
        public void UnauthenticatedRequestThrowsException() {
            
            _server
                .Given(Request.Create().WithPath("/v0/tags").UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(401)
                        .WithBody("{}")
                );
            
            Exception ex = Assert.Throws<Exceptions.UnauthorizedException>(() => 
                _client.Tag<string>()
                    .WithKey("urn:tagspace:tag")
                    .WithResource("http://some.resource.url")
                    .WithValue("some value")
                    .Apply());
                 
            Assert.Equal("Your request was not properly authenticated", ex.Message);
        }
        
        [Fact]
        public void UnauthorizedRequestThrowsException() {
            
            _server
                .Given(Request.Create().WithPath("/v0/tags").UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(403)
                        .WithBody("{}")
                );
            
            Exception ex = Assert.Throws<Exceptions.ForbiddenException>(() => 
                _client.Tag<string>()
                    .WithKey("urn:tagspace:tag")
                    .WithResource("http://some.resource.url")
                    .WithValue("some value")
                    .Apply());
                 
            Assert.Equal("Your don't have access to perform the action", ex.Message);
        }
       
        [Fact]
        public void TagPerformsAllTheLowLevelCalls() {
            
            _server
                .Given(Request.Create().WithPath("/v0/tags").UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBodyAsJson(new
                            {
                            resourceUri = "http://some.resource",
                            key = "urn:my-service:tag",
                            value = "bla bla bla",
                            createdAt = "2018-11-01T09:27:17+00:00",
                            createdBy = "auth0|a767ex734cv376vx346xv34",
                            modifiedAt = "2018-11-01T09:27:18+00:00",
                            modifiedBy = "auth0|sdfv454353543534534534"
                            })
                    );
            
            _client.Tag<string>().WithKey("urn:tagspace:tag").WithResource("http://some.resource.url").WithValue("some value").Apply();

            var logEntry = _server.FindLogEntries(
                Request.Create()
                    .WithPath("/v0/tags")
                    .UsingPost())
                .FirstOrDefault();
            var obj = (JObject) logEntry.RequestMessage.BodyAsJson;
            Assert.NotNull(logEntry);
            Assert.Equal(obj["key"].ToString(), "urn:tagspace:tag");
            Assert.Equal(obj["value"].ToString(), "some value");
            Assert.Equal(obj["resourceUri"].ToString(), "http://some.resource.url");
        }

        [Fact]
        public void UntagPerformsAllTheLowLevelCalls()
        {

            _server
                .Given(Request.Create().WithPath("/v0/tags")
                    .WithParam("key", "urn:tagspace:tag")
                    .WithParam("resourceUri", "http://some.resource.url")
                    .UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBodyAsJson(new
                        {
                            total = 1,
                            results = new[]
                            {
                                new
                                {
                                    resourceUri = "http://some.resource",
                                    key = "urn:my-service:tag",
                                    id = "abrakadabra1234"
                                }
                            }
                        })
        );

        _server
                .Given(Request.Create().WithPath("/v0/tags/abrakadabra1234").UsingDelete())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBody("{}")
                );
            
            _client.Tag<string>()
                .WithKey("urn:tagspace:tag")
                .WithResource("http://some.resource.url")
                .WithValue("some value")
                .Remove();

            var logEntryGet = _server.FindLogEntries(
                    Request.Create()
                        .WithPath("/v0/tags")
                        .UsingGet())
                .FirstOrDefault();
            Assert.NotNull(logEntryGet);
            
            var logEntryDelete = _server.FindLogEntries(
                    Request.Create()
                        .WithPath("/v0/tags/abrakadabra1234")
                        .UsingDelete())
                .FirstOrDefault();
            Assert.NotNull(logEntryDelete);
          }
    }
}
