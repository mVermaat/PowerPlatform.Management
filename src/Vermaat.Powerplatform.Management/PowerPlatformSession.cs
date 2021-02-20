using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management
{
    public class PowerPlatformSession
    {
        private const string _audience = "https://management.azure.com/";

        public string Token { get; private set; }

        private PowerPlatformSession(AuthenticationResult authInfo)
        {
            Token = authInfo.AccessToken;
        }

        public static async Task<PowerPlatformSession> CreateFromClientCredentialsAsync(EndpointInfo endpointInfo, string tenant, string clientId, string clientSecret)
        {
            var authContext = new AuthenticationContext($"{endpointInfo.BaseUri}/{tenant}/oauth2/authorize");
            var credential = new ClientCredential(clientId, clientSecret);
            var result = await authContext.AcquireTokenAsync(_audience, credential);
            return new PowerPlatformSession(result);
        }
    }
}
