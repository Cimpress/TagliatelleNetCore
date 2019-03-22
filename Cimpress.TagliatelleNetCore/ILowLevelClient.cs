﻿using System.Threading.Tasks;
using Cimpress.TagliatelleNetCore.Data;
using RestSharp;

namespace Cimpress.TagliatelleNetCore
{
    /// <summary>
    /// Low level client captures atomic service calls and is not abstracting
    /// the underlying technology that is used to communicate with the service
    /// </summary>
    public interface ILowLevelClient
    {
        /// <summary>
        /// Gets all tags matching the criteria
        /// </summary>
        /// <param name="key"></param>
        /// <param name="resourceUri"></param>
        /// <returns></returns>
        Task<IRestResponse<TagBulkResponse>> getTags(string key, string resourceUri);

        /// <summary>
        /// Posts a tag
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IRestResponse<TagResponse>> postTag(TagRequest request);

        /// <summary>
        /// Updates tag with specific tag id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IRestResponse<TagResponse>> putTag(string id, TagRequest request);

        /// <summary>
        /// Deletes tag with specific tag id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IRestResponse> deleteTag(string id);
    }
}