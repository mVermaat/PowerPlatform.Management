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
            var response = await SendRequest<EnvironmentCollectionJsonModel, string>(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments?$expand=permissions&api-version={_apiVersion}")
            });

            if (response.Success)
                return response.Content.ToEnvironmentCollection();
            else
                throw new InvalidOperationException($"Error: {response.StatusCode}: {response.Error}");
        }

        public async Task<Models.PowerAutomate[]> GetPowerAutomates(Models.Environment environment)
        {
            var response = await SendRequest<PowerAutomateCollectionJsonModel, string>(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            });

            if (response.Success)
                return response.Content.ToPowerAutomateCollection();
            else
                throw new InvalidOperationException($"Error: {response.StatusCode}: {response.Error}");
        }

        public async Task<Models.PowerAutomateRun[]> GetPowerAutomateRunHistory(Models.Environment environment, Models.PowerAutomate powerAutomate)
        {
            var response = await SendRequest<PowerAutomateRunCollectionJsonModel, string>(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows/{powerAutomate.PowerAppIdentifier}/runs?api-version={_apiVersion}")
            });

            if (response.Success)
                return response.Content.ToPowerAutomateCollection();
            else
                throw new InvalidOperationException($"Error: {response.StatusCode}: {response.Error}");
        }
    }
}
