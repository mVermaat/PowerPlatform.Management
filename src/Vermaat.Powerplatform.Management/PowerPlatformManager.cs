using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management
{
    public abstract class PowerPlatformManager : IDisposable
    {
        protected const string _apiVersion = "2016-11-01";

        protected TokenManager _tokenManager { get; private set; }
        protected HttpClient _httpClient { get; private set; }
        protected EndpointInfo _endpointInfo { get; private set; }

        private bool _disposedValue;

        public PowerPlatformManager(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _endpointInfo = tokenManager.EndpointInfo;
            _httpClient = new HttpClient();
        }

        protected async Task<TConverted> SendRequest<TSuccess, TConverted>(HttpRequestMessage request, 
            Func<TSuccess, TConverted> convertSuccessFunc)
        {
            var response = await SendRequest<TSuccess, string>(request);

            if (response.Success)
                return convertSuccessFunc(response.Content);
            else
                throw new InvalidOperationException($"API returned {response.StatusCode}: {response.Error}");
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
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                _disposedValue = true;
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
