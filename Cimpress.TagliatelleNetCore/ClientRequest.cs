using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cimpress.TagliatelleNetCore.Data;
using Cimpress.TagliatelleNetCore.Exceptions;
using RestSharp;

namespace Cimpress.TagliatelleNetCore
{
    public class ClientRequest<T> : IClientRequest<T> 
    {
        private const string TagliatelleUrl = "https://tagliatelle.trdlnk.cimpress.io/";

        /// <summary>
        /// Request object holding all properties of the tag
        /// </summary>
        private readonly TagRequest<T> _tagRequest = new TagRequest<T>();

        /// <summary>
        /// Low level client abstraction for invoking commands on the API
        /// </summary>
        private ILowLevelClient<T> _lowLevelClient;

        public ClientRequest(string accessToken, string urlOverride = null) {
            _lowLevelClient = new LowLevelClient<T>(accessToken, urlOverride ?? TagliatelleUrl);
        }

        public IClientRequest<T> WithResource(string resourceUri)
        {
            _tagRequest.ResourceUri = resourceUri;
            return this;
        }

        public IClientRequest<T> WithKey(string tagKey)
        {
            _tagRequest.Key = tagKey;
            return this;
        }
        
        public IClientRequest<T> WithValue(string value)
        {
            _tagRequest.Value = value;
            return this;        
        }
        public IClientRequest<T> WithValueAsObject(T value)
        {
            _tagRequest.ValueAsObject = value;
            return this;        
        }

        public void Apply()
        {
            HandleOperationTag();
        }

        private void HandleOperationTag()
        {
            var response = _lowLevelClient.postTag(_tagRequest).Result;
            HandleErrorCondition(response);
            if (response.StatusCode != HttpStatusCode.Conflict) return;
            var bulkResponse = _lowLevelClient.getTags(_tagRequest.Key, _tagRequest.ResourceUri).Result;
            if (bulkResponse.Data.Total != 1) {
                throw new Exception("Unable to update the tag");
            }
            var existingTag = bulkResponse.Data.Results[0];
            _lowLevelClient.putTag(existingTag.Id, _tagRequest);
        }

        public void Remove()
        {
            handleOperationUntag();
        }

        private void handleOperationUntag()
        {
            var bulkResponse = _lowLevelClient.getTags(_tagRequest.Key, _tagRequest.ResourceUri).Result;
            HandleErrorCondition(bulkResponse);
            var bulkResponseResults = bulkResponse.Data;
            var tasks = bulkResponseResults.Results.Select(r => _lowLevelClient.deleteTag(r.Id)).Cast<Task>().ToArray();
            Task.WaitAll(tasks);
        }

        public TagBulkResponse<T> Fetch()
        {
            return HandleOperationFetch();
        }

        private TagBulkResponse<T> HandleOperationFetch()
        {
            var bulkResponse = _lowLevelClient.getTags(_tagRequest.Key, _tagRequest.ResourceUri).Result;
            HandleErrorCondition(bulkResponse);
            return bulkResponse.Data;
        }
        
        private void HandleErrorCondition(IRestResponse response) {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch(response.StatusCode) {
                case HttpStatusCode.Unauthorized: 
                    throw new UnauthorizedException("Your request was not properly authenticated");
                case HttpStatusCode.Forbidden: 
                    throw new ForbiddenException("Your don't have access to perform the action");
                case HttpStatusCode.BadRequest: 
                    throw new MalfomedTagException("The tag is malformed");
            }
        }
    }
}
