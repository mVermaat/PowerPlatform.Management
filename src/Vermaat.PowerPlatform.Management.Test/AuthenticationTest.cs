using FluentAssertions;
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

            var token = tokenManager.GetToken().Result;
            token.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void TestUserPasswordAuthentication()
        {
            var tokenManager = new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password);
            var token = tokenManager.GetToken().Result;
            token.Should().NotBeNullOrEmpty();
        }
    }
}
