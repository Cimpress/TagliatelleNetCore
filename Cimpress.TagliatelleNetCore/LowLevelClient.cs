using System.Collections.Generic;
using System.Threading.Tasks;
using Cimpress.TagliatelleNetCore.Data;
using RestSharp;
using RestSharp.Serializers;

namespace Cimpress.TagliatelleNetCore
{
    /// <inheritdoc />
    public class LowLevelClient<T> : ILowLevelClient<T>

    {
        private const string TagliatelleUrl = "https://tagliatelle.trdlnk.cimpress.io/";

        private readonly IRestClient _restClient;

        public LowLevelClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public LowLevelClient(string urlOverride = null)
        {
            _restClient = new RestClient(urlOverride ?? TagliatelleUrl);
        }

        public Task<IRestResponse<TagBulkResponse<T>>> getTags(string accessToken, string key, string resourceUri)
        {
            return this.getTags(accessToken, key, new List<string> { resourceUri });
        }


        /// <inheritdoc />
        public Task<IRestResponse<TagBulkResponse<T>>> getTags(string accessToken, string key, IList<string> resourceUris)
        {
            var request = new RestRequest("v0/tags", Method.GET);
            request.JsonSerializer = new JsonSerializer();

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            if (resourceUris != null)
            {
                foreach (var resourceUri in resourceUris)
                {
                    if(resourceUri != null){
                        request.AddParameter("resourceUri", resourceUri);
                    }
                }
            }

            if (key != null)
            {
                request.AddParameter("key", key);
            }

            return _restClient.ExecuteTaskAsync<TagBulkResponse<T>>(request);
        }

        /// <inheritdoc />
        public Task<IRestResponse<TagResponse<T>>> postTag(string accessToken, TagRequest<T> tagRequest)
        {
            var request = new RestRequest("v0/tags", Method.POST);
            request.JsonSerializer = new JsonSerializer();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddJsonBody(tagRequest);
            return _restClient.ExecuteTaskAsync<TagResponse<T>>(request);
        }

        /// <inheritdoc />
        public Task<IRestResponse<TagResponse<T>>> putTag(string accessToken, string id, TagRequest<T> tagRequest)
        {
            var request = new RestRequest("v0/tags/{id}", Method.PUT);
            request.JsonSerializer = new JsonSerializer();
            request.AddUrlSegment("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddJsonBody(tagRequest);
            return _restClient.ExecuteTaskAsync<TagResponse<T>>(request);
        }

        /// <inheritdoc />
        public Task<IRestResponse> deleteTag(string accessToken, string id)
        {
            var request = new RestRequest("v0/tags/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            return _restClient.ExecuteTaskAsync(request);
        }
    }
}