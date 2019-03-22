using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cimpress.TagliatelleNetCore.Data
{   
    public class TagBulkResponse<T> 
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("total")]
        public int Total { get; set; }
        
        [JsonProperty("offset")]
        public string Offset { get; set; }
        
        [JsonProperty("results")]
        public List<TagResponse<T>> Results { get; set; }
    }
}