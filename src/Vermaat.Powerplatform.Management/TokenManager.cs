using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management
{
    public abstract class TokenManager
    {
        protected const string _audience = "https://management.azure.com/";
        protected const string _application = "1950a258-227b-4e31-a9cf-717495945fc2";

        protected EndpointInfo EndpointInfo { get; private set; }

        public TokenManager(EndpointInfo endpointInfo)
        {
            EndpointInfo = endpointInfo;
        }

        protected abstract Task<AuthenticationResult> GetNewToken();
        
    }
}
