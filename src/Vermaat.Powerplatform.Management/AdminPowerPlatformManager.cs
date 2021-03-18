using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.JsonModels;
using Vermaat.PowerPlatform.Management.Models;
using Environment = Vermaat.PowerPlatform.Management.Models.Environment;


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
                RequestUri = new Uri($"https://{EndpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments?$expand=permissions&api-version={_apiVersion}")
            };

            return await SendRequest<EnvironmentCollectionJsonModel, Models.Environment[]>(message, m => m.ToEnvironmentCollection());
        }

        public async Task<Models.PowerAutomate[]> GetPowerAutomates(Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            };

            return await SendRequest<PowerAutomateCollectionJsonModel, PowerAutomate[]>(message, m => m.ToPowerAutomateCollection());
        }

        public async Task DisablePowerAutomate(Environment environment, PowerAutomate powerAutomate)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows/{powerAutomate.PowerAppIdentifier}/stop?api-version={_apiVersion}")
            };

            await SendRequest(message);
        }

        public async Task EnablePowerAutomate(Environment environment, PowerAutomate powerAutomate)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows/{powerAutomate.PowerAppIdentifier}/start?api-version={_apiVersion}")
            };

            await SendRequest(message);
        }

        public async Task<PowerAutomateRun[]> GetPowerAutomateRunHistory(Environment environment, PowerAutomate powerAutomate)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows/{powerAutomate.PowerAppIdentifier}/runs?api-version={_apiVersion}")
            };

            return await SendRequest<PowerAutomateRunCollectionJsonModel, PowerAutomateRun[]>(message, m => m.ToPowerAutomateCollection());
        }

        public async Task<Connection[]> GetConnections(Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/scopes/admin/environments/{environment.PowerAppIdentifier}/connections?api-version={_apiVersion}")
            };

            return await SendRequest<ConnectionsCollectionJsonModel, Connection[]>(message, m => m.ToConnectionCollection());
        }
    }
}
