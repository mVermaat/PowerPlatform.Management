using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class AuthenticationTest : BaseTest
    {
        [TestMethod]
        public void TestClientSecretAuthentication()
        {
            var session = PowerPlatformSession.CreateFromClientCredentialsAsync(EndpointInfo.Prod, TenantId, ClientId, ClientSecret).Result;
            var manager = new PowerPlatformManager(session, EndpointInfo.Prod);
            manager.GetEnvironments();
        }

        [TestMethod]
        public void TestUserPasswordAuthentication()
        {
            var session = PowerPlatformSession.CreateFromUsernamePasswordAsync(EndpointInfo.Prod, Username, Password).Result;
            var manager = new PowerPlatformManager(session, EndpointInfo.Prod);
            manager.GetEnvironments();
        }
    }
}
