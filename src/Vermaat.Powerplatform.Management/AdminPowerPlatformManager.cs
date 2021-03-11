using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.JsonModels;

namespace Vermaat.PowerPlatform.Management
{
    public class AdminPowerPlatformManager : PowerPlatformManager
    {
        public AdminPowerPlatformManager(TokenManager tokenManager)
            : base(tokenManager)
        {

        }

        public async Task<Models.Environment[]> GetEnvironments()
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments?$expand=permissions&api-version={_apiVersion}")
            };

            return await SendRequest<EnvironmentCollectionJsonModel, Models.Environment[]>(message, m => m.ToEnvironmentCollection());
        }

        public async Task<Models.PowerAutomate[]> GetPowerAutomates(Models.Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            };

            return await SendRequest<PowerAutomateCollectionJsonModel, Models.PowerAutomate[]>(message, m => m.ToPowerAutomateCollection());
        }

        public async Task<Models.PowerAutomateRun[]> GetPowerAutomateRunHistory(Models.Environment environment, Models.PowerAutomate powerAutomate)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows/{powerAutomate.PowerAppIdentifier}/runs?api-version={_apiVersion}")
            };

            return await SendRequest<PowerAutomateRunCollectionJsonModel, Models.PowerAutomateRun[]>(message, m => m.ToPowerAutomateCollection());
        }
    }
}
