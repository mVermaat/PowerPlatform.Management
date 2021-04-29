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
                RequestUri = new Uri($"https://{EndpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/environments?`$expand=permissions&api-version={_apiVersion}")
            };

            return await SendRequest<EnvironmentCollectionJsonModel, Environment[]>(message, m => m.ToEnvironmentCollection());
        }

        public async Task<PowerAutomate[]> GetPowerAutomates(Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            };

            return await SendRequest<PowerAutomateCollectionJsonModel, PowerAutomate[]>(message, m => m.ToPowerAutomateCollection());
        }

        public async Task<Connector[]> GetConnectors(Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/apis?api-version={_apiVersion}&$filter=environment eq '{environment.PowerAppIdentifier}'")
            };

            return await SendRequest<ConnectorsCollectionJsonModel, Connector[]>(message, m => m.ToConnectorCollection());
        }
       
        public async Task<Connection[]> GetConnections(Environment environment)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/connections?api-version={_apiVersion}&$filter=environment eq '{environment.PowerAppIdentifier}'")
            };

            return await SendRequest<ConnectionsCollectionJsonModel, Connection[]>(message, m => m.ToConnectionCollection());
        }

        public async Task CreateConnection(Environment environment, Connection connection)
        {
            string payload = JsonConvert.SerializeObject(new CreateConnectionJsonModel(environment), JsonSerializeSettings);
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/apis/{connection.Type}/connections/{connection.Id}/?api-version=2020-06-01&$filter=environment%20eq%20%27{environment.PowerAppIdentifier}%27"),
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };

            await SendRequest(message);
        }

        public async Task DeleteConnection(Environment environment, Connection connection)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://{EndpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/apis/{connection.Type}/connections/{connection.Id}?api-version=2020-06-01&$filter=environment%20eq%20%27{environment.PowerAppIdentifier}%27"),
            };

            await SendRequest(message);
        }

    }
}
