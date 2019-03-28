using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cimpress.TagliatelleNetCore
{
    public class Client : IClient
    {
        private readonly string _tagliatelleUrl = null;

        public Client(string urlOverride = null) {
            this._tagliatelleUrl = urlOverride;
        }

        public IClientRequest<JObject> Tag(string accessToken) 
        {
            return new ClientRequest<JObject>(accessToken, _tagliatelleUrl);
        }
        
        public IClientRequest<T> Tag<T>(string accessToken) 
        {
            return new ClientRequest<T>(accessToken, _tagliatelleUrl);
        }
    }
}
