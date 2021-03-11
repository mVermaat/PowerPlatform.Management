using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class GetPowerAutomatesTest : BaseTest
    {
        [TestMethod]
        public async Task TestGetPowerAutomates()
        {
            var manager = new AdminPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var actualPowerAutomates = await manager.GetPowerAutomates(TestEnvironment);

            actualPowerAutomates.Should().HaveCount(PowerAutomates.Count);

            foreach (var actualPowerAutomate in actualPowerAutomates)
            {
                ShouldHaveMatch(actualPowerAutomate, false);
            }
        }

        [TestMethod]
        public async Task TestEnableDisable()
        {
            var manager = new AdminPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));

            await manager.DisablePowerAutomate(TestEnvironment, TestPowerAutomate);
            TestPowerAutomate.State = PowerAutomateState.Stopped;

            var actual = (await manager.GetPowerAutomates(TestEnvironment)).First(pa => pa.PowerAppIdentifier.Equals(TestPowerAutomate.PowerAppIdentifier));
            ShouldMatch(TestPowerAutomate, actual);

            await manager.EnablePowerAutomate(TestEnvironment, TestPowerAutomate);
            TestPowerAutomate.State = PowerAutomateState.Started;
            actual = (await manager.GetPowerAutomates(TestEnvironment)).First(pa => pa.PowerAppIdentifier.Equals(TestPowerAutomate.PowerAppIdentifier));
            ShouldMatch(TestPowerAutomate, actual);
        }


        private void ShouldHaveMatch(PowerAutomate actualPowerAutomate, bool checkStage)
        {
            var expected = PowerAutomates.FirstOrDefault(pa => pa.PowerAppIdentifier.Equals(actualPowerAutomate.PowerAppIdentifier));
            expected.Should().NotBeNull($"PA {expected.DisplayName} shouldn't exist");

            ShouldMatch(expected, actualPowerAutomate, checkStage);
        }

        private void ShouldMatch(PowerAutomate expected, PowerAutomate actual, bool checkState = true)
        {
            actual.DataverseIdentifier.Should().Be(expected.DataverseIdentifier);
            actual.DisplayName.Should().BeEquivalentTo(expected.DisplayName);

            if (checkState)
                actual.State.Should().Be(expected.State);
        }
    }
}
