using System;
using System.Collections.Generic;
using System.Text;

namespace Vermaat.PowerPlatform.Management
{
    public class EndpointInfo
    {
        public string BaseUri { get; set; }
        public string BapEndpoint { get; set; }






        public static EndpointInfo Prod
            => new EndpointInfo()
            {
                BaseUri = "https://login.windows.net",
                BapEndpoint = "api.bap.microsoft.com"
            };
    }
}
