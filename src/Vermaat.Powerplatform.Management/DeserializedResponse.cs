using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
