using Newtonsoft.Json.Linq;

namespace Cimpress.TagliatelleNetCore
{
    public interface IClient
    {
        IClientRequest<T> Tag<T>() ;
        
        IClientRequest<JObject> Tag() ;
    }
}