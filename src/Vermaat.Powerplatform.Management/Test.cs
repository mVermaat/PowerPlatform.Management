using System;
using System.Collections.Generic;
using System.Text;

namespace Vermaat.Powerplatform.Management
{
    class Test
    {
        public void Test()
        {
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSercret = ConfigurationManager.AppSettings["ClientSecret"];

            var baseUrl = "https://login.windows.net";
            var tenant = "e1089ea4-45e8-4613-892c-1bd51a95a785";

            var authContext = new AuthenticationContext($"{baseUrl}/{tenant}/oauth2/authorize");
            var credential = new ClientCredential(clientId, clientSercret);
            var audience = "https://management.azure.com/";

            var authResult = authContext.AcquireTokenAsync(audience, credential).Result;
        }
    }
}
