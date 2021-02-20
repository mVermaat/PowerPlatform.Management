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
        private const string _application = "1950a258-227b-4e31-a9cf-717495945fc2";

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

        public static async Task<PowerPlatformSession> CreateFromUsernamePasswordAsync(EndpointInfo endpointInfo, string userName, string password)
        {
            var authContext = new AuthenticationContext($"{endpointInfo.BaseUri}/common");
            var credential = new UserPasswordCredential(userName, password);
            var result = await authContext.AcquireTokenAsync(_audience, _application, credential);
            return new PowerPlatformSession(result);
        }
    }
}
