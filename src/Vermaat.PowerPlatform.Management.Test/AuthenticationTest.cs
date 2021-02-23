using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class AuthenticationTest : BaseTest
    {
        [TestMethod]
        public void TestClientSecretAuthentication()
        {
            var tokenManager = new ClientCredentialTokenManager(EndpointInfo.Prod, TenantId, ClientId, ClientSecret);
            var manager = new PowerPlatformManager(tokenManager);
            manager.GetEnvironments();
        }

        [TestMethod]
        public void TestUserPasswordAuthentication()
        {
            var tokenManager = new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password);
            var manager = new PowerPlatformManager(tokenManager);
            manager.GetEnvironments();
        }
    }
}
