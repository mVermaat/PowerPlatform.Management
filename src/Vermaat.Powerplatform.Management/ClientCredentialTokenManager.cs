using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management
{
    public class ClientCredentialTokenManager : TokenManager
    {
        private readonly Guid _tenantId;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public ClientCredentialTokenManager(EndpointInfo endpointInfo, Guid tenantId, string clientId, string clientSecret) : base(endpointInfo)
        {
            _tenantId = tenantId;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        protected override async Task<AuthenticationResult> GetNewToken()
        {
            var authContext = new AuthenticationContext($"{EndpointInfo.BaseUri}/{_tenantId:D}/oauth2/authorize");
            var credential = new ClientCredential(_clientId, _clientSecret);
            return await authContext.AcquireTokenAsync(_audience, credential);
        }
    }
}
