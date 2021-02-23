using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Vermaat.PowerPlatform.Management
{
    public class PowerPlatformManager : IDisposable
    {
        private const string _apiVersion = "2016-11-01";

        private readonly TokenManager _tokenManager;
        private readonly HttpClient _httpClient;
        
        private bool disposedValue;

        public PowerPlatformManager(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _httpClient = new HttpClient();

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Token);
        }

        public void GetDefaultEnvironment()
        {
            //var result = _httpClient.GetAsync($"https://{_endpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/environments/~default?`$expand=permissions&api-version={_apiVersion}").Result;
        }

        public void GetEnvironments()
        {
            //var result = _httpClient.GetAsync($"https://{_endpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/environments?`$expand=permissions&api-version={_apiVersion}").Result;
            //var result = _httpClient.GetAsync($"https://{_endpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments?$expand=permissions&api-version={_apiVersion}").Result;

            //var content = result.Content.ReadAsStringAsync().Result;

            //if (!result.IsSuccessStatusCode)
            //    throw new Exception(content);
            
        }

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
    }
}
