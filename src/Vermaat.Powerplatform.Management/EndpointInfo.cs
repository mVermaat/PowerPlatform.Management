namespace Vermaat.PowerPlatform.Management
{
    public class EndpointInfo
    {
        public string BaseUri { get; set; }
        public string BapEndpoint { get; set; }
        public string PowerAppEndpoint { get; set; }
        public string PowerAutomateEndpoint { get; set; }






        public static EndpointInfo Prod
            => new EndpointInfo()
            {
                BaseUri = "https://login.windows.net",
                BapEndpoint = "api.bap.microsoft.com",
                PowerAppEndpoint = "api.powerapps.com",
                PowerAutomateEndpoint = "api.flow.microsoft.com"
            };
    }
}
