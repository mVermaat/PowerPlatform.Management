using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.JsonModels;

namespace Vermaat.PowerPlatform.Management
{
    public class AdminPowerPlatformManager : IDisposable
    {
        private const string _apiVersion = "2016-11-01";

        private readonly TokenManager _tokenManager;
        private readonly HttpClient _httpClient;
        private readonly EndpointInfo _endpointInfo;

        private bool disposedValue;

        public AdminPowerPlatformManager(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _endpointInfo = tokenManager.EndpointInfo;
            _httpClient = new HttpClient();
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

        public async Task<string> GetPowerAutomates(Models.Environment environment)
        {
            var response = await SendRequest<string, string>(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{_endpointInfo.PowerAutomateEndpoint}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environment.PowerAppIdentifier}/flows?api-version={_apiVersion}")
            });

            if (response.Success)
                return response.Content;
            else
                throw new InvalidOperationException($"Error: {response.StatusCode}: {response.Error}");
        }

        private async Task<DeserializedResponse<TSuccess, TError>> SendRequest<TSuccess, TError>(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenManager.GetToken());

            var response = await _httpClient.SendAsync(request);
            var result = new DeserializedResponse<TSuccess, TError>()
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode
            };

            if (result.Success)
                result.Content = Deserialize<TSuccess>(await response.Content.ReadAsStringAsync());
            else
                result.Error = Deserialize<TError>(await response.Content.ReadAsStringAsync());

            return result;
        }

        private T Deserialize<T>(string text)
        {
            if (typeof(T) == typeof(string))
                return (T)(object)text;
            else
                return JsonConvert.DeserializeObject<T>(text);
        }

        #region IDisposible

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
