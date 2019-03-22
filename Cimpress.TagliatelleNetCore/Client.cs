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
        private readonly string _accessToken = null;
        private readonly string _tagliatelleUrl = null;

        public Client(string accessToken) {
            _accessToken = accessToken;
        }


        public Client(string accessToken, string urlOverride) : this(accessToken) {
            this._tagliatelleUrl = urlOverride;
        }

        public IClientRequest<JObject> Tag() 
        {
            return new ClientRequest<JObject>(_accessToken, _tagliatelleUrl);
        }
        
        public IClientRequest<T> Tag<T>() 
        {
            return new ClientRequest<T>(_accessToken, _tagliatelleUrl);
        }
    }
}
