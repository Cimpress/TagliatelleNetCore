using System;
using System.Collections.Generic;
using Cimpress.TagliatelleNetCore.Data;

namespace Cimpress.TagliatelleNetCore
{
    public interface IClientRequest<T> 
    {
        /// <summary>
        /// Fluent command for specifying the resource the tag should refer to
        /// </summary>
        /// <param name="resourceUri">Resource url the tag should be attached to.</param>
        /// <returns></returns>
        IClientRequest<T> WithResource(string resourceUri);

        /// <summary>
        /// Fluent command for specifying the key of the tag
        /// </summary>
        /// <param name="tagKey">Key of the tag is a user provided URN that describes the tag. The fact that that the key is a URN, gives freedom for the user to model hierarchy and other aspects of their tag</param>
        /// <returns></returns>
        IClientRequest<T> WithKey(string tagKey);

        /// <summary>
        /// Optionally it is possible to attach opaque metadata to the tag.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IClientRequest<T> WithValue(string value);
        
        /// <summary>
        /// Optionally it is possible to attach opaque metadata to the tag.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IClientRequest<T> WithValueAsObject(T value);

        /// <summary>
        /// Apply the tag to the fluently configured conditions
        /// </summary>
        void Apply();

        /// <summary>
        /// Remove tags matching fluently configured conditions 
        /// </summary>
        void Remove();

        /// <summary>
        /// Fetch tags matching fluently configured conditions
        /// </summary>
        /// <returns></returns>
        TagBulkResponse<T> Fetch();
    }
}