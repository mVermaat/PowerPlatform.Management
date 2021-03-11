using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Environment = Vermaat.PowerPlatform.Management.Models.Environment;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class EnvironmentTests : BaseTest
    {
        [TestMethod]
        public async Task TestGetAdminEnvironments()
        {
            var manager = new AdminPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var environments = await manager.GetEnvironments();

            ShouldMatch(environments);
        }

        [TestMethod]
        public async Task TestGetEnvironments()
        {
            var manager = new MakerPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var environments = await manager.GetEnvironments();
            ShouldMatch(environments);
        }

        private void ShouldMatch(Environment[] actualEnvironments)
        {
            actualEnvironments.Should().NotBeNull();
            actualEnvironments.Should().HaveCount(Environments.Count);

            foreach (var expectedEnvironment in Environments)
            {
                var actualEnvironment = actualEnvironments.FirstOrDefault(e => e.PowerAppIdentifier.Equals(expectedEnvironment.PowerAppIdentifier, StringComparison.OrdinalIgnoreCase));
                actualEnvironment.Should().NotBeNull($"Environment {expectedEnvironment.PowerAppIdentifier} doesn't exist");

                actualEnvironment.DataverseIdentifier.Should().Be(expectedEnvironment.DataverseIdentifier);
                actualEnvironment.DataverseUrl.Should().BeEquivalentTo(expectedEnvironment.DataverseUrl);
                actualEnvironment.IsDefault.Should().Be(expectedEnvironment.IsDefault);
                actualEnvironment.Name.Should().BeEquivalentTo(expectedEnvironment.Name);
            }
        }

    }
}
