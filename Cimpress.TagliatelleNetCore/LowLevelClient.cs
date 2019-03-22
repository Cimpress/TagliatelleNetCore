using System.Threading.Tasks;
using Cimpress.TagliatelleNetCore.Data;
using RestSharp;
using RestSharp.Serializers;

namespace Cimpress.TagliatelleNetCore
{
    /// <inheritdoc />
    public class LowLevelClient : ILowLevelClient
    {
        private const string TagliatelleUrl = "https://tagliatelle.trdlnk.cimpress.io/";
        
        private readonly string _accessToken;
        
        private readonly IRestClient _restClient;

        public LowLevelClient(string accessToken, IRestClient restClient = null)
        {
            _accessToken = accessToken;
            _restClient = restClient;
        }

        public LowLevelClient(string accessToken, string urlOverride = null)
        {
            _accessToken = accessToken;
            _restClient = new RestClient(urlOverride ?? TagliatelleUrl);
        }

        /// <inheritdoc />
        public Task<IRestResponse<TagBulkResponse>> getTags(string key, string resourceUri)
        { 
            var request = new RestRequest("v0/tags", Method.GET);
            request.JsonSerializer = new JsonSerializer();

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");
            if (resourceUri != null) {
                request.AddParameter("resourceUri", resourceUri); 
            }
            if (key != null) {
                request.AddParameter("key", key);
            }

            return _restClient.ExecuteTaskAsync<TagBulkResponse>(request);
        }

        /// <inheritdoc />
        public Task<IRestResponse<TagResponse>> postTag(TagRequest tagRequest)
        {
            var request = new RestRequest("v0/tags", Method.POST);
            request.JsonSerializer = new JsonSerializer();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");
            request.AddJsonBody(tagRequest);
            return _restClient.ExecuteTaskAsync<TagResponse>(request);
        }

        /// <inheritdoc />
        public Task<IRestResponse<TagResponse>> putTag(string id, TagRequest tagRequest)
        {
            var request = new RestRequest("v0/tags/{id}", Method.PUT);
            request.JsonSerializer = new JsonSerializer();
            request.AddUrlSegment("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");
            request.AddJsonBody(tagRequest);
            return _restClient.ExecuteTaskAsync<TagResponse>(request);
        }

        /// <inheritdoc />
        public Task<IRestResponse> deleteTag(string id)
        {
            var request = new RestRequest("v0/tags/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_accessToken}");
            return _restClient.ExecuteTaskAsync(request);
        }
    }
}