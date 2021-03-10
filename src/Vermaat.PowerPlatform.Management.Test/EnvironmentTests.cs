using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class EnvironmentTests : BaseTest
    {
        [TestMethod]
        public async Task TestGetAdminEnvironments()
        {
            var manager = new PowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var environments = await manager.GetAdminEnvironments();

            environments.Should().NotBeNull();

            foreach (var environment in environments)
            {
                environment.PowerAppIdentifier.Should().NotBeNullOrWhiteSpace();
                environment.Name.Should().NotBeNullOrWhiteSpace();

                if (environment.IsDefault)
                {
                    environment.DataverseIdentifier.Should().BeNull();
                    environment.DataverseUrl.Should().BeNull();
                }
                else
                {
                    environment.DataverseIdentifier.Should().HaveValue().And.NotBe(Guid.Empty);
                    environment.DataverseUrl.Should().NotBeNullOrWhiteSpace();
                }
            }
        }

        [TestMethod]
        public async Task TestGetEnvironments()
        {
            var manager = new PowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var environments = await manager.GetEnvironments();

            environments.Should().NotBeNull();

            foreach (var environment in environments)
            {
                environment.PowerAppIdentifier.Should().NotBeNullOrWhiteSpace();
                environment.Name.Should().NotBeNullOrWhiteSpace();

                if (environment.IsDefault)
                {
                    environment.DataverseIdentifier.Should().BeNull();
                    environment.DataverseUrl.Should().BeNull();
                }
                else
                {
                    environment.DataverseIdentifier.Should().HaveValue().And.NotBe(Guid.Empty);
                    environment.DataverseUrl.Should().NotBeNullOrWhiteSpace();
                }
            }
        }
    }
}
