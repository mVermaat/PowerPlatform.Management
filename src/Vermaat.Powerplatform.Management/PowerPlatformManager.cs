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

        private readonly PowerPlatformSession _session;
        private readonly EndpointInfo _endpointInfo;
        private readonly HttpClient _httpClient;
        
        private bool disposedValue;

        public PowerPlatformManager(PowerPlatformSession session, EndpointInfo endpointInfo)
        {
            _session = session;
            _endpointInfo = endpointInfo;
            _httpClient = new HttpClient();
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Token);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public void GetEnvironments()
        {
            //var result = _httpClient.GetAsync($"https://{_endpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/environments?`$expand=permissions&api-version={_apiVersion}").Result;
            //var result = _httpClient.GetAsync($"https://{_endpointInfo.PowerAppEndpoint}/providers/Microsoft.PowerApps/environments?`$expand=permissions&api-version={_apiVersion}").Result;
            var result = _httpClient.GetAsync($"https://{_endpointInfo.BapEndpoint}/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments?$expand=permissions&api-version={_apiVersion}").Result;

            var content = result.Content.ReadAsStringAsync().Result;

            if (!result.IsSuccessStatusCode)
                throw new Exception(content);
            
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
