using System;
using Newtonsoft.Json;

namespace Cimpress.TagliatelleNetCore.Data
{   
    public class TagRequest<T> 
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")] 
        public string Value { get; set; }
            
        [JsonIgnore]
        public T ValueAsObject
        {
            set => Value = JsonConvert.SerializeObject(value);
        }

        [JsonProperty("resourceUri")]
        public string ResourceUri { get; set; }
    }
}