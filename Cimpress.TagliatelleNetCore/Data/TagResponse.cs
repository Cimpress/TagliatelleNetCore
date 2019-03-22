using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cimpress.TagliatelleNetCore.Data
{
    public class TagResponse : TagRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("modifiedAt")]
        public string ModifiedAt { get; set; }

        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("_links")]
        public IDictionary<string, object> Links { get; set; }   
    }
}