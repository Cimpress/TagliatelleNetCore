using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cimpress.TagliatelleNetCore.Data
{
    public class TagBulkResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("total")]
        public int Total { get; set; }
        
        [JsonProperty("offset")]
        public string Offset { get; set; }
        
        [JsonProperty("results")]
        public IList<TagResponse> Results { get; set; }
    }
}