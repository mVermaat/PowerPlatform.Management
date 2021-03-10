using System.Net;

namespace Vermaat.PowerPlatform.Management
{
    internal class DeserializedResponse<TSuccess, TError>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public TSuccess Content { get; set; }
        public TError Error { get; set; }
    }
}
