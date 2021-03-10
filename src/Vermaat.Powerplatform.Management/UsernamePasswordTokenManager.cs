using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management
{
    public class UsernamePasswordTokenManager : TokenManager
    {
        private readonly string _username;
        private readonly string _password;

        public UsernamePasswordTokenManager(EndpointInfo endpointInfo, string username, string password) : base(endpointInfo)
        {
            _username = username;
            _password = password;
        }

        protected override async Task<AuthenticationResult> GetNewToken()
        {
            var authContext = new AuthenticationContext($"{EndpointInfo.BaseUri}/common");
            var credential = new UserPasswordCredential(_username, _password);
            return await authContext.AcquireTokenAsync(_audience, _application, credential);
        }

    }
}
