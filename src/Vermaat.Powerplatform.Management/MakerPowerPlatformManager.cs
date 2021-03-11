using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.JsonModels;
using Vermaat.PowerPlatform.Management.Models;

using Environment = Vermaat.PowerPlatform.Management.Models.Environment;

namespace Vermaat.PowerPlatform.Management
{
    public class MakerPowerPlatformManager : PowerPlatformManager
    {

        public MakerPowerPlatformManager(TokenManager tokenManager)
            : base(tokenManager)
        {

        }


        public async Task<Environment[]> GetEnvironments()
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/environments?`$expand=permissions&api-version={_apiVersion}")
            };

            return await SendRequest<EnvironmentCollectionJsonModel, Environment[]>(message, m => m.ToEnvironmentCollection());
        }

        public async Task<PowerAutomate[]> GetPowerAutomates(Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            };

            return await SendRequest<PowerAutomateCollectionJsonModel, PowerAutomate[]>(message, m => m.ToPowerAutomateCollection());
        }

    }
}
