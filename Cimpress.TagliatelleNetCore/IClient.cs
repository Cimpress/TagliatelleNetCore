using Newtonsoft.Json.Linq;

namespace Cimpress.TagliatelleNetCore
{
    public interface IClient
    {
        IClientRequest<T> Tag<T>(string accessToken) ;
        
        IClientRequest<JObject> Tag(string accessToken) ;
    }
}