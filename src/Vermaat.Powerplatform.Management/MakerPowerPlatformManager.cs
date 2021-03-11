using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.JsonModels;

namespace Vermaat.PowerPlatform.Management
{
    public class MakerPowerPlatformManager : PowerPlatformManager
    {

        public MakerPowerPlatformManager(TokenManager tokenManager)
            : base(tokenManager)
        {

        }

        public async Task<Models.Environment[]> GetAdminEnvironments()
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

        public async Task<Models.Environment[]> GetEnvironments()
        {
            var response = await SendRequest<EnvironmentCollectionJsonModel, string>(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/environments?`$expand=permissions&api-version={_apiVersion}")
            });

            if (response.Success)
                return response.Content.ToEnvironmentCollection();
            else
                throw new InvalidOperationException($"Error: {response.StatusCode}: {response.Error}");
        }

        public async Task<string> GetPowerAutomates(Models.Environment environment)
        {
            // "https://{flowEndpoint}/providers/Microsoft.ProcessSimple/environments/{environment}/flows/{flowName}?api-version={apiVersion}"
            //var response = await SendRequest<string, string>(new HttpRequestMessage()
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            //});
            var response = await SendRequest<string, string>(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            });

            if (response.Success)
                return response.Content;
            else
                throw new InvalidOperationException($"Error: {response.StatusCode}: {response.Error}");
        }


    }
}
