using System;
using Newtonsoft.Json;

namespace Cimpress.TagliatelleNetCore.Data
{   
    public class TagRequest<T> : TagRequest
    {
        [JsonIgnore]
        public T ValueObject
        {
            get
            {
                try
                {
                    return (T) JsonConvert.DeserializeObject(Value);
                }
                catch (Exception e)
                {
                    return default(T);
                }
            }
            set => Value = JsonConvert.SerializeObject(value);
        }
    }
    
    public class TagRequest
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]

        public string Value { get; set; }

        [JsonProperty("resourceUri")]
        public string ResourceUri { get; set; }
    }
}