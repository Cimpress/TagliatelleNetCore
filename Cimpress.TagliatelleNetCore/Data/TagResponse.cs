using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cimpress.TagliatelleNetCore.Data
{
    public class TagResponse<T> : TagRequest<T> 
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
        public Dictionary<string, object> Links { get; set; }   
        
        public new T ValueAsObject => (T) JsonConvert.DeserializeObject(Value, typeof(T));
    }
}