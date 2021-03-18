using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class ConnectionsTest : BaseTest
    {
        [TestMethod]
        public async Task TestConnections()
        {
            var manager = new MakerPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var existingConnections = (await manager.GetConnections(TestEnvironment)).ToList();
            
            var connection = new Connection()
            {
                Id = Guid.NewGuid().ToString("N"),
                DisplayName = "Common Data Service (current environment)",
                Type = "shared_commondataserviceforapps"
            };
            await manager.CreateConnection(TestEnvironment, connection);

            var actualConnectionsPreDelete = await manager.GetConnections(TestEnvironment);
            await manager.DeleteConnection(TestEnvironment, connection);
            var actualConnectionsPostDelete = await manager.GetConnections(TestEnvironment);

            HasMatch(actualConnectionsPostDelete, existingConnections);
            existingConnections.Add(connection);
            HasMatch(actualConnectionsPreDelete, existingConnections);
        }

        private void HasMatch(Connection[] actualConnections, List<Connection> expectedConnections)
        {
            actualConnections.Should().NotBeNull();
            actualConnections.Should().HaveCount(expectedConnections.Count);

            foreach (var expectedConnection in expectedConnections)
            {
                var actualConnection = actualConnections.FirstOrDefault(e => e.Id.Equals(expectedConnection.Id, StringComparison.OrdinalIgnoreCase));
                actualConnection.Should().NotBeNull($"Connection {expectedConnection.Id} doesn't exist");

                actualConnection.AccountName.Should().BeEquivalentTo(expectedConnection.AccountName);
                actualConnection.DisplayName.Should().BeEquivalentTo(expectedConnection.DisplayName);
                actualConnection.Type.Should().Be(expectedConnection.Type);
            }
        }
    }
}
